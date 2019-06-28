Imports System
Imports System.Collections.Generic
Imports System.Runtime.InteropServices
Imports Microsoft.Win32

Imports HResult = System.Int32

Namespace DirectOutputVBWrapper
    Public Structure SRequestStatus
        Public headerError As Integer
        Public headerInfo As Integer
        Public requestError As Integer
        Public requestInfo As Integer
    End Structure

    Public Class RegistryKeyNotFound : Inherits Exception
        Public Sub New()
            MyBase.New("HKEY_LOCAL_MACHINE\SOFTWARE\Saitek\DirectOutput key not found.")
        End Sub
    End Class

    Public Class RegistryValueNotFound : Inherits Exception
        Public Sub New()
            MyBase.New("DirectOutput value in key HKEY_LOCAL_MACHINE\SOFTWARE\Saitek\DirectOutput not found.")
        End Sub
    End Class


    Public Class HResultException : Inherits Exception
        Public Const S_OK As HResult = &H0
        Public Const E_OUTOFMEMORY As HResult = CType(&H8007000E, HResult)
        Public Const E_NOTIMPL As HResult = CType(&H80004001, HResult)
        Public Const E_INVALIDARG As HResult = CType(&H80070057, HResult)
        Public Const E_PAGENOTACTIVE As HResult = CType(&HFF040001, HResult)
        Public Const E_HANDLE As HResult = CType(&H80070006, HResult)
        Public Const E_UNKNOWN_1 As HResult = CType(&H51B87CE3, HResult)

        Public Sub New(ByVal result As HResult, ByVal errorsMap As Dictionary(Of HResult, String))
            MyBase.New(errorsMap(result))
            HResult = result
        End Sub
    End Class
End Namespace

Public Class DirectOutputVB
    Public Const IsActive As Integer = &H1

    'Callbacks
    Public Delegate Sub EnumarateCallback(ByVal device As IntPtr, ByVal target As IntPtr)
    Public Delegate Sub DeviceCallback(ByVal device As IntPtr, ByVal added As Boolean, ByVal target As IntPtr)
    Public Delegate Sub SoftButtonCallback(ByVal device As IntPtr, ByVal buttons As UInteger, ByVal target As IntPtr)
    Public Delegate Sub PageCallback(ByVal device As IntPtr, ByVal page As UInteger, ByVal activated As Boolean, ByVal target As IntPtr)

    'Library functions
    Private Delegate Function DirectOutput_Initialize(<MarshalAsAttribute(UnmanagedType.LPWStr)> ByVal appName As String) As HResult
    Private Delegate Function DirectOutput_Deinitialize() As HResult
    Private Delegate Function DirectOutput_RegisterDeviceCallback(<MarshalAs(UnmanagedType.FunctionPtr)> ByVal callback As DeviceCallback, ByVal target As IntPtr) As HResult


    Private Delegate Function DirectOutput_Enumerate(<MarshalAs(UnmanagedType.FunctionPtr)> ByVal callback As EnumarateCallback, ByVal target As IntPtr) As HResult
    Private Delegate Function DirectOutput_GetDeviceType(ByVal device As IntPtr, <Out> ByRef guidType As Guid) As HResult
    Private Delegate Function DirectOutput_GetDeviceInstance(ByVal device As IntPtr, <Out> ByRef guidInstance As Guid) As HResult

    Private Delegate Function DirectOutput_AddPage(ByVal device As IntPtr, ByVal page As Int32, ByVal flags As Int32) As HResult
    Private Delegate Function DirectOutput_RemovePage(ByVal device As IntPtr, ByVal page As Int32) As HResult
    Private Delegate Function DirectOutput_SetLed(ByVal device As IntPtr, ByVal page As Int32, ByVal index As Int32, ByVal value As Int32) As HResult

    'Functions placeholders
    Private _initialize As DirectOutput_Initialize
    Private _deinitialize As DirectOutput_Deinitialize
    Private _registerDeviceCallback As DirectOutput_RegisterDeviceCallback

    Private _enumerate As DirectOutput_Enumerate
    Private _getDeviceType As DirectOutput_GetDeviceType
    Private _getDeviceInstance As DirectOutput_GetDeviceInstance

    Private _addPage As DirectOutput_AddPage
    Private _removePage As DirectOutput_RemovePage
    Private _setLed As DirectOutput_SetLed

    Private Const directOutputKey As String = "SOFTWARE\Saitek\DirectOutput"
    Private hModule As IntPtr

    Public Sub New(ByVal Optional libPath As String = Nothing)
        If libPath Is Nothing Then
            Dim key As RegistryKey = Registry.LocalMachine.OpenSubKey(directOutputKey)
            If key Is Nothing Then
                Throw New Exception
            End If

            Dim value As String = key.GetValue("DirectOutput")
            If (value Is Nothing) OrElse Not (TypeOf value Is String) Then
                Throw New Exception
            End If

            libPath = value.ToString
        End If

        hModule = DirectOutputVBWrapper.DllHelper.LoadLibrary(libPath)

        InitializeLibraryFunctions()
    End Sub

    Private Sub InitializeLibraryFunctions()
        _initialize = DirectOutputVBWrapper.DllHelper.GetFunction(Of DirectOutput_Initialize)(hModule, "DirectOutput_Initialize")
        _deinitialize = DirectOutputVBWrapper.DllHelper.GetFunction(Of DirectOutput_Deinitialize)(hModule, "DirectOutput_Deinitialize")
        _registerDeviceCallback = DirectOutputVBWrapper.DllHelper.GetFunction(Of DirectOutput_RegisterDeviceCallback)(hModule, "DirectOutput_RegisterDeviceCallback")

        _enumerate = DirectOutputVBWrapper.DllHelper.GetFunction(Of DirectOutput_Enumerate)(hModule, "DirectOutput_Enumerate")
        _getDeviceType = DirectOutputVBWrapper.DllHelper.GetFunction(Of DirectOutput_GetDeviceType)(hModule, "DirectOutput_GetDeviceType")
        _getDeviceInstance = DirectOutputVBWrapper.DllHelper.GetFunction(Of DirectOutput_GetDeviceInstance)(hModule, "DirectOutput_GetDeviceInstance")

        _addPage = DirectOutputVBWrapper.DllHelper.GetFunction(Of DirectOutput_AddPage)(hModule, "DirectOutput_AddPage")
        _removePage = DirectOutputVBWrapper.DllHelper.GetFunction(Of DirectOutput_RemovePage)(hModule, "DirectOutput_RemovePage")
        _setLed = DirectOutputVBWrapper.DllHelper.GetFunction(Of DirectOutput_SetLed)(hModule, "DirectOutput_SetLed")
    End Sub

    ''' <summary>
    ''' Initialize the DirectOutput library.
    ''' </summary>
    ''' <param name="appName">String that specifies the name of the application. Optional</param>
    ''' <remarks>
    ''' This function must be called before calling any others. Call this function when you want to initialize the DirectOutput library.
    ''' </remarks>
    ''' <exception cref="DirectOutputVBWrapper.HResultException"></exception>
    Public Sub Initialize(ByVal Optional appName As String = "DirectOutputCSharpWrapper")
        Dim retVal As HResult = _initialize(appName)

        If retVal <> DirectOutputVBWrapper.HResultException.S_OK Then
            Dim errorsMap As Dictionary(Of HResult, String) = New Dictionary(Of HResult, String)() From {
                {DirectOutputVBWrapper.HResultException.E_OUTOFMEMORY, "There was insufficient memory to complete this call."},
                {DirectOutputVBWrapper.HResultException.E_INVALIDARG, "The argument is invalid."},
                {DirectOutputVBWrapper.HResultException.E_HANDLE, "The DirectOutputManager prcess could not be found."}
            }
            Throw New DirectOutputVBWrapper.HResultException(retVal, errorsMap)
        End If
    End Sub

    Public Sub Deinitialize()
        Dim retVal As HResult = _deinitialize()

        If retVal <> DirectOutputVBWrapper.HResultException.S_OK Then
            Dim errorsMap As Dictionary(Of HResult, String) = New Dictionary(Of HResult, String)() From {
                {DirectOutputVBWrapper.HResultException.E_HANDLE, "DirectOutput was not initialized or was already deinitialized."}
            }
            Throw New DirectOutputVBWrapper.HResultException(retVal, errorsMap)
        End If
    End Sub

    Public Sub RegisterDeviceCallback(ByVal callback As DeviceCallback)
        Dim retVal As HResult = _registerDeviceCallback(callback, New IntPtr())

        If retVal <> DirectOutputVBWrapper.HResultException.S_OK Then
            Dim errorsMap As Dictionary(Of HResult, String) = New Dictionary(Of HResult, String)() From {
            {DirectOutputVBWrapper.HResultException.E_HANDLE, "DirectOutput was not initialized."}
        }
            Throw New DirectOutputVBWrapper.HResultException(retVal, errorsMap)
        End If
    End Sub

    Public Sub Enumerate(ByVal callback As EnumarateCallback)
        Dim retVal As HResult = _enumerate(callback, New IntPtr())

        If retVal <> DirectOutputVBWrapper.HResultException.S_OK Then
            Dim errorsMap As Dictionary(Of HResult, String) = New Dictionary(Of HResult, String)() From {
                {DirectOutputVBWrapper.HResultException.E_HANDLE, "DirectOutput was not initialized."}
            }
            Throw New DirectOutputVBWrapper.HResultException(retVal, errorsMap)
        End If
    End Sub

    Public Function GetDeviceType(ByVal device As IntPtr) As Guid
        Dim guidType As Guid
        Dim retVal As HResult = _getDeviceType(device, guidType)

        If retVal <> DirectOutputVBWrapper.HResultException.S_OK Then
            Dim errorsMap As Dictionary(Of HResult, String) = New Dictionary(Of HResult, String)() From {
                {DirectOutputVBWrapper.HResultException.E_INVALIDARG, "An argument is invalid."},
                {DirectOutputVBWrapper.HResultException.E_HANDLE, "The device handle specified is invalid."}
            }
            Throw New DirectOutputVBWrapper.HResultException(retVal, errorsMap)
        End If

        Return guidType
    End Function

    Public Function GetDeviceInstance(ByVal device As IntPtr) As Guid
        Dim guidType As Guid
        Dim retVal As HResult = _getDeviceInstance(device, guidType)

        If retVal <> DirectOutputVBWrapper.HResultException.S_OK Then
            Dim errorsMap As Dictionary(Of HResult, String) = New Dictionary(Of HResult, String)() From {
                {DirectOutputVBWrapper.HResultException.E_NOTIMPL, "This device does not support DirectInput."},
                {DirectOutputVBWrapper.HResultException.E_INVALIDARG, "An argument is invalid."},
                {DirectOutputVBWrapper.HResultException.E_HANDLE, "The device handle specified is invalid."}
            }
            Throw New DirectOutputVBWrapper.HResultException(retVal, errorsMap)
        End If

        Return guidType
    End Function

    Public Sub AddPage(ByVal device As IntPtr, ByVal page As Int32, ByVal flags As Int32)
        Dim retVal As HResult = _addPage(device, page, flags)

        If retVal <> DirectOutputVBWrapper.HResultException.S_OK Then
            Dim errorsMap As Dictionary(Of HResult, String) = New Dictionary(Of HResult, String)() From {
                {DirectOutputVBWrapper.HResultException.E_OUTOFMEMORY, "Insufficient memory to complete the request."},
                {DirectOutputVBWrapper.HResultException.E_INVALIDARG, "The page parameter already exists."},
                {DirectOutputVBWrapper.HResultException.E_HANDLE, "The device handle specified is invalid."}
            }
            Throw New DirectOutputVBWrapper.HResultException(retVal, errorsMap)
        End If
    End Sub

    Public Sub RemovePage(ByVal device As IntPtr, ByVal page As Int32)
        Dim retVal As HResult = _removePage(device, page)

        If retVal <> DirectOutputVBWrapper.HResultException.S_OK Then
            Dim errorsMap As Dictionary(Of HResult, String) = New Dictionary(Of HResult, String)() From {
                {DirectOutputVBWrapper.HResultException.E_INVALIDARG, "The page parameter argument does not reference a valid page id."},
                {DirectOutputVBWrapper.HResultException.E_HANDLE, "The device handle specified is invalid."}
            }
            Throw New DirectOutputVBWrapper.HResultException(retVal, errorsMap)
        End If
    End Sub

    Public Sub SetLed(ByVal device As IntPtr, ByVal page As Int32, ByVal index As Int32, ByVal value As Int32)
        Dim retVal As HResult = _setLed(device, page, index, value)

        If retVal <> DirectOutputVBWrapper.HResultException.S_OK Then
            Dim errorsMap As Dictionary(Of HResult, String) = New Dictionary(Of HResult, String)() From {
                {DirectOutputVBWrapper.HResultException.E_PAGENOTACTIVE, "The specified page is not active. Displaying information is not permitted when the page is not active."},
                {DirectOutputVBWrapper.HResultException.E_INVALIDARG, "The dwPage argument does not reference a valid page id, or the dwIndex argument does not specifiy a valid LED id."},
                {DirectOutputVBWrapper.HResultException.E_HANDLE, "The device handle specified is invalid."}
            }
            Throw New DirectOutputVBWrapper.HResultException(retVal, errorsMap)
        End If
    End Sub
End Class

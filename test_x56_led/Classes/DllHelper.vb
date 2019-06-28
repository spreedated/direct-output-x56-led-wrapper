Imports System
Imports System.ComponentModel
Imports System.Runtime.InteropServices

Namespace DirectOutputVBWrapper
    Class DllHelper
        <DllImport("kernel32.dll", EntryPoint:="LoadLibrary")>
        Private Shared Function _LoadLibrary(ByVal dllPath As String) As IntPtr
        End Function
        <DllImport("kernel32.dll", EntryPoint:="GetProcAddress")>
        Private Shared Function _GetProcAddress(ByVal hModule As IntPtr, ByVal procedureName As String) As IntPtr
        End Function
        <DllImport("kernel32.dll", EntryPoint:="FreeLibrary")>
        Private Shared Function _FreeLibrary(ByVal hModule As IntPtr) As Boolean
        End Function
        <DllImport("kernel32.dll", EntryPoint:="GetLastError")>
        Private Shared Function _GetLastError() As Integer
        End Function

        Public Shared Function LoadLibrary(ByVal dllPath As String) As IntPtr
            Dim moduleHandle As IntPtr = _LoadLibrary(dllPath)

            If moduleHandle = New IntPtr(0) Then
                Throw New OutOfMemoryException()
            ElseIf moduleHandle = New IntPtr(2) Then
                Throw New Exception
            ElseIf moduleHandle = New IntPtr(3) Then
                Throw New Exception
            ElseIf moduleHandle = New IntPtr(11) Then
                Throw New Exception
            End If

            Return moduleHandle
        End Function

        Public Shared Function GetFunction(Of T As Class)(ByVal hModule As IntPtr, ByVal procedureName As String) As T
            Dim addressOfFunctionToCall As IntPtr = _GetProcAddress(hModule, procedureName)

            If addressOfFunctionToCall = IntPtr.Zero Then
                Throw New Win32Exception(_GetLastError())
            End If

            Dim functionDelegate As [Delegate] = Marshal.GetDelegateForFunctionPointer(addressOfFunctionToCall, GetType(T))
            Return TryCast(functionDelegate, T)
        End Function

        Public Shared Sub FreeLibrary(ByVal hModule As IntPtr)
            Dim isLibraryUnloadedSuccessfully As Boolean = _FreeLibrary(hModule)

            If Not isLibraryUnloadedSuccessfully Then
                Throw New Win32Exception(_GetLastError())
            End If
        End Sub
    End Class

End Namespace



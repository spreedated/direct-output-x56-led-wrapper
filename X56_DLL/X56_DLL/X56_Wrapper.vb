Public Class X56_Wrapper
    Private _DeviceHandles As List(Of (Guid, IntPtr)) = New List(Of (Guid, IntPtr))
    Public Property DeviceHandles As List(Of (Guid, IntPtr))
        Set(value As List(Of (Guid, IntPtr)))
            _DeviceHandles = value
        End Set
        Get
            Return _DeviceHandles
        End Get
    End Property

    Private _EnumReady As Boolean = False
    Public ReadOnly Property EnumReady As Boolean
        Get
            Return _EnumReady
        End Get
    End Property

    Enum DeviceSelection
        Stick
        Throttle
        Both
    End Enum

    Private dcObj As DirectOutputVB = New DirectOutputVB
    Private cbFuncEnumarateDevice As DirectOutputVB.EnumarateCallback
    Private cbFuncNewDevice As DirectOutputVB.DeviceCallback

    Private Sub EnumCallback(device As IntPtr, target As IntPtr)
        ' GUIDs
        ' {EE9E44A9-46CD-470B-B650-9C5859992240} == THROTTLE
        ' {40D17E5E-8091-409F-A2B5-24EF1915A812} == STICK
        DeviceHandles.Add((dcObj.GetDeviceType(device), device))
        Debug.Print(dcObj.GetDeviceType(device).ToString)

        _EnumReady = True
    End Sub
    Private Sub DeviceCallback(ByVal device As IntPtr, ByVal added As Boolean, ByVal target As IntPtr)
        Debug.Print("Callback from device: " & dcObj.GetDeviceType(device).ToString)
        Dim fnd As Boolean = False
        For Each dcv In DeviceHandles
            If dcv.Item1 = dcObj.GetDeviceType(device) Then
                'need to be improved to actually update the DeviceHandles List (adding & removing items)
                fnd = True
            End If
        Next
        If Not fnd Then
            DeviceHandles.Add((dcObj.GetDeviceType(device), device))
        End If
    End Sub
    Public Function Open()
        Try
            'Enumaration of DirectInput devices
            dcObj.Initialize()
            cbFuncEnumarateDevice = AddressOf EnumCallback
            dcObj.Enumerate(cbFuncEnumarateDevice)
            cbFuncNewDevice = AddressOf DeviceCallback
            dcObj.RegisterDeviceCallback(cbFuncNewDevice)
        Catch ex As Exception
            Debug.Print(ex.Message)
            Return False
        End Try
        Return True
    End Function
    Public Function Close()
        Try
            dcObj.Deinitialize()
        Catch ex As Exception
            Debug.Print(ex.Message)
            Return False
        End Try
        Return True
    End Function
    Public Overloads Function SetLed(ByVal device As DeviceSelection, ByVal Red As Short, ByVal Green As Short, ByVal Blue As Short, ByVal Optional brightness As Short = 100)
        Do While _EnumReady = False
            'noop
        Loop

        Try
            Select Case device
                Case 0 'Stick
                    For Each dvc In DeviceHandles
                        If dvc.Item1.ToString.ToLower.StartsWith("40D17E5E".ToLower) Then
                            dcObj.AddPage(dvc.Item2, 1, 1)
                            dcObj.SetLed(dvc.Item2, 1, 0, brightness)
                            dcObj.SetLed(dvc.Item2, 1, 1, ColorCoding.RGB_to_Int32(Red, Green, Blue))
                            dcObj.RemovePage(dvc.Item2, 1)
                        End If
                    Next
                Case 1 'Throttle
                    For Each dvc In DeviceHandles
                        If dvc.Item1.ToString.ToLower.StartsWith("EE9E44A9".ToLower) Then
                            dcObj.AddPage(dvc.Item2, 1, 1)
                            dcObj.SetLed(dvc.Item2, 1, 0, brightness)
                            dcObj.SetLed(dvc.Item2, 1, 1, ColorCoding.RGB_to_Int32(Red, Green, Blue))
                            dcObj.RemovePage(dvc.Item2, 1)
                        End If
                    Next
                Case 2 'Both
                    For Each dvc In DeviceHandles
                        dcObj.AddPage(dvc.Item2, 1, 1)
                        dcObj.SetLed(dvc.Item2, 1, 0, brightness)
                        dcObj.SetLed(dvc.Item2, 1, 1, ColorCoding.RGB_to_Int32(Red, Green, Blue))
                        dcObj.RemovePage(dvc.Item2, 1)
                    Next
            End Select
        Catch ex As Exception
            Debug.Print(ex.Message)
            Return False
        End Try
        Return True
    End Function

    Public Overloads Function SetLed(ByVal device As DeviceSelection, ByVal color As Integer, ByVal Optional brightness As Short = 100)
        Do While _EnumReady = False
            'noop
        Loop

        Try
            Select Case device
                Case 0 'Stick
                    For Each dvc In DeviceHandles
                        If dvc.Item1.ToString.ToLower.StartsWith("40D17E5E".ToLower) Then
                            dcObj.AddPage(dvc.Item2, 1, 1)
                            dcObj.SetLed(dvc.Item2, 1, 0, brightness)
                            dcObj.SetLed(dvc.Item2, 1, 1, color)
                            dcObj.RemovePage(dvc.Item2, 1)
                        End If
                    Next
                Case 1 'Throttle
                    For Each dvc In DeviceHandles
                        If dvc.Item1.ToString.ToLower.StartsWith("EE9E44A9".ToLower) Then
                            dcObj.AddPage(dvc.Item2, 1, 1)
                            dcObj.SetLed(dvc.Item2, 1, 0, brightness)
                            dcObj.SetLed(dvc.Item2, 1, 1, color)
                            dcObj.RemovePage(dvc.Item2, 1)
                        End If
                    Next
                Case 2 'Both
                    For Each dvc In DeviceHandles
                        dcObj.AddPage(dvc.Item2, 1, 1)
                        dcObj.SetLed(dvc.Item2, 1, 0, brightness)
                        dcObj.SetLed(dvc.Item2, 1, 1, color)
                        dcObj.RemovePage(dvc.Item2, 1)
                    Next
            End Select
        Catch ex As Exception
            Debug.Print(ex.Message)
            Return False
        End Try
        Return True
    End Function
End Class

Public Class ColorCoding
    Public Shared Function RGB_to_Int32(ByVal Red As UShort, ByVal Green As UShort, ByVal Blue As UShort) As Integer
        RGB_to_Int32 = ((256 ^ 2) * Red) + (256 * Green) + Blue
        Return RGB_to_Int32
    End Function

    Public Shared Function Int32_to_RGB(ByVal Base As Integer) As List(Of Short)
        Int32_to_RGB = New List(Of Short)
        Dim Red As Short = Base / (256 ^ 2)
        Dim Green As Short = (Base / 256) Mod 256
        Dim Blue As Short = Base Mod 256
        Int32_to_RGB.Add(Red)
        Int32_to_RGB.Add(Green)
        Int32_to_RGB.Add(Blue)
        Return Int32_to_RGB
    End Function
End Class
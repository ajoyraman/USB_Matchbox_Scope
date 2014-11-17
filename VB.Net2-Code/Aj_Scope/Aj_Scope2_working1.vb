Imports System
Imports System.IO.Ports
Imports System.Globalization
Imports ZedGraph
Imports System.Math

Public Class Scope_Main_Form
    'Definations
#Region "Definations"
#Region "Definitions for Connect to COM Port "
    'Definitions for COM Port Connection
    'Define  serial port instance
    Dim mPort As New SerialPort
    Dim myBuffer As Integer = 255
    'myBuffer = mPort.ReadBufferSize SET in Sub Test_232
#End Region
#Region "Definations for comPort_Setup and test"
    Public Shared selected_com_port As String
    'Gets possible serial port names and puts in listbox
    Dim myTestReadString As String 'Used by Test 232 & other readbacks
#End Region
#Region "Definations for Command Strings"
    Dim TestLEDcmd() As Byte = {&H74} 't for Test LED
    Dim myTestHex() As Byte = {&H49} 'I for Identify
    Dim TrigSourceCmd() As Byte = {&H54, &H0} ' T Trig Source Auto/CH1/CH2 0/1/2
    Dim TrigPolarityCmd() As Byte = {&H50, &H0} ' P Trig Polarity L/H H/L 0/1
    Dim ReadVrefCmd() As Byte = {&H52} ' R  Read Reference Voltahe 3.3V
    Dim SetGainCmd() As Byte = {&H47, &H0, &H0} ' G CH1/CH2 Gains 1/2/5 0/1/3
    Dim SetCH1_Offset() As Byte = {&H4F, &H2, &H0} ' O msb-lsb
    Dim SetCH2_Offset() As Byte = {&H6F, &H2, &H0} ' o msb-lsb
    Dim SetTrigValue() As Byte = {&H4C, &H2, &H0} ' L msb-lsb
    Dim SetModeCmd() As Byte = {&H46, &H0} 'F Normal/CH1/Ch2 0/1/2
    Dim SetSampleRateCmd() As Byte = {&H53, &H1} 'S rate 1 to 20
    Dim CaptureCmd() As Byte = {&H43} 'C Capture Data
    Dim ReadDataCmd() As Byte = {&H44, &H1} 'D 0/1/2 CH1&CH2/CH1 FFT/CH2 FFT
    Dim DFT As Boolean = False 'False/True Normal Data Plot /DFT mode
    Dim AbortCmd() As Byte = {&H41} 'A Abort



    Dim myAnalog_Offset1() As Byte = {&H4F, &H31, &H2, &H0, &HD} 'O1
    Dim myAnalog_Trig1() As Byte = {&H4F, &H32, &H2, &H0, &HD} 'O2
    Dim myAnalog_Offset2() As Byte = {&H4F, &H33, &H2, &H0, &HD} 'O3
    Dim myAnalog_Trig2() As Byte = {&H4F, &H34, &H2, &H0, &HD} 'O4
    Dim myDigital_Cmd() As Byte = {&H44, &H4F, &HB5, &H4, &HD} '
    Dim myRun_Cmd() As Byte = {&H41, &H42, &H2, &H12, &HD} '
    Dim myRead_Cmd() As Byte = {&H41, &H39, &H1, &H1, &HD} '
    Dim myRead_Vref() As Byte = {&H41, &H33, &H1, &H1, &HD}
    Dim myHex_Freq(3) As Byte
    Dim myHex_Amp(1) As Byte
    Dim myHex_Offset(1) As Byte
    'Command String
    Dim myCommandHex() As Byte = {&H31, &H0, &HA3, &HD7, &H1F, &H80} 'Default
    Dim Frequency As String
    Dim Phase_Step, PhstepB0, PhstepB1, PhstepB2 As Integer
#End Region
#Region "Gain, Offset and Trigger Level, Vref"
    Dim Ch1_Offset, Ch2_Offset, Ch1_Trig, Ch2_Trig As Integer 'raw Values
    Dim Ch1_Offset1, Ch2_Offset1, Ch1_Trig1, Ch2_Trig1 As Integer 'Corrected values
    Dim Ch1_Gain, Ch2_Gain As Integer
    Dim Scale_Factor, OC1, Vref, OC2, Offset_Gain_Factor As String

    Dim Vref_array(5) As Byte
#End Region
#Region "Other variables"
    Dim ETS, ETS_Error As Integer 'used for ETS  Error and selecting Continuous/sliding modes
    Dim Keep_Running As Boolean = False
    Dim Overplot As Boolean = False
    Dim Heading1 As String
    Dim Multiplier As String
    Dim Time_array(400), CH1_DFT_Array(400), CH2_DFT_Array(400), Ch1_dataArray(400), Ch2_dataArray(400) As Byte
    Dim DataArray(401, 5) As String
    Dim Data_Length As Integer
    Dim Ack_array(4) As Byte
    Dim Max_DFT As Double


#End Region
#Region "Definitions For Plot data"
    'Dim myPane As GraphPane = Aj_ZG1.GraphPane
    Dim xcolumn, ycolumn, y1column, Max_Frequency As Integer
    Dim display_x, display_y, display_y1 As Boolean
    Dim loc As New Point(0, 140)
    Dim Plot_Error, Sq_Display, DFT_Display As Boolean

    'Dim myRow1 As DataRow
    'Dim myColumn1, myColumn2 As DataColumn
    'Dim colNoX, colNoY, ColNoY2 As Byte 'rowNo
    'Dim ID_Start, ID_End As Byte
    'Dim Input_Text, Output_Text As String

#End Region
#End Region
#Region "Main Form Load"
    Private Sub Scope_Main_Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Maxemize Display
        Me.WindowState = FormWindowState.Maximized
        Sample_Rate_ListBox.Text = "100kbps  10us/sample"
        'Sample_Rate_ListBox.Text = "100kbps  10us/sample"
    End Sub
#End Region
#Region "Connect To COM Port"
    Sub GetSerialPortNames()
        ' Show all available COM ports.
        'Get SP data and write to ListBox
        For Each sp As String In My.Computer.Ports.SerialPortNames
            Com_Ports.Items.Add(sp)
        Next
    End Sub
    'Sub called to get serial port names
    Private Sub Get_Ports_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Get_Ports_Button.Click
        'Calls GetSerialPortnames()
        GetSerialPortNames()
        Test_TextBox.Text = "Select Com Port"
    End Sub
    'shows available serial ports
    'on select removes other names from list
    'calls initialise of mPort with selected name
    Private Sub Available_Ports_ListBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Com_Ports.SelectedIndexChanged
        RemoveOtherItems()
        Initialize_232()
        Test_232()
        ''which_port_TextBox.Text = selected_com_port
    End Sub
    'Removes Other than selected Item from Available_Ports_ListBox
    Private Sub RemoveOtherItems()
        Dim selected_index, item_count, x As Integer
        ' Determine index value of selected item
        selected_index = Com_Ports.SelectedIndex
        item_count = Com_Ports.Items.Count

        selected_com_port = Com_Ports.Items(selected_index)

        ' Clear all items below selected
        For x = (item_count - 1) To (selected_index + 1) Step -1
            Com_Ports.Items.RemoveAt(x)
        Next x

        selected_index = Com_Ports.SelectedIndex
        item_count = Com_Ports.Items.Count

        ' Remove all items above selected item in the ListBox.
        For x = (selected_index - 1) To 0 Step -1
            Com_Ports.Items.RemoveAt(x)
        Next x
        '' Clear all selections in the ListBox.
        'Available_Ports_ListBox.ClearSelected()
        ' Remove all items below now top item in the ListBox.
    End Sub
    'Initializes selected port
    Private Sub Initialize_232()
        'Close the port before defining parameters
        If mPort.IsOpen Then
            mPort.Close()
        End If
        'COM port number selected
        mPort.PortName = selected_com_port
        'Speed of your link. 
        mPort.BaudRate = 115200
        'Data Terminal Ready signal. 
        'It's better to set this enable.
        mPort.DtrEnable = True
        'Data Ready to Send signal. 
        'It's better to set this enable.
        mPort.RtsEnable = True
        'Optional, If you want to send AT commands.
        mPort.NewLine = Chr(13)
        'Open the port if not already open
        If mPort.IsOpen Then
            'do nothing
        Else
            mPort.Open()
        End If
        'By Default Bits=8, Stop Bits=1, Parity=None
    End Sub

    'Test Communication with SigGen Hardware
    Private Sub Test_232()
        'Dim myTestReadString As String 'defined earlier
        myBuffer = mPort.ReadBufferSize
        mPort.DiscardInBuffer() 'Clear Input Buffer
        mPort.Write(myTestHex, 0, 1)  ' Write Test Command


        Try
            mPort.ReadTimeout = 500
            Do
                myTestReadString = mPort.ReadLine
                If myTestReadString Is Nothing Then
                    Exit Do
                Else
                    Test_TextBox.Text = myTestReadString
                    Exit Try
                End If
            Loop
        Catch ex As TimeoutException
            Test_TextBox.Text = "Time Out."
            mPort.Close()
            'Finally
            'If mPort IsNot Nothing Then mPort.Close()
        End Try
    End Sub

#End Region


#Region "Setup Gain, Offset , Trigger Level & VREF"
    Private Sub Compute_Offset_Trigger()
        'Dim Ch1_Offset, Ch2_Offset, Ch1_Trig, Ch2_Trig As Integer 'raw Values
        'Dim Ch1_Offset1, Ch2_Offset1, Ch1_Trig1, Ch2_Trig1 As Integer 'Corrected values
        'Dim Ch1_Gain, Ch2_Gain, Add_Ch1_offset, Add_Ch2_Offset As Integer
        'Dim Scale_Factor As Integer
        'Dim Vref_array(5) As byte

        mPort.DiscardInBuffer() 'Important to Clear Input Buffer 
        mPort.Write(ReadVrefCmd, 0, 1) 'write the command for reading Vref 4 values
        While (mPort.BytesToRead = 0)
        End While
        System.Threading.Thread.Sleep(30) 'sleep 30mSec Reqd before every read
        mPort.Read(Vref_array, 0, 5) 'First Byte is 'R' then AN2 msb-lsb /AN3 msb-lsb

        Vref = Vref_array(1) * 256 + Vref_array(2) + Vref_array(3) * 256 + Vref_array(4)
        Vref = Vref / 2 'Average of AN2 & AN3

        Scale_Factor = Math.Round(672 / Vref, 3) '672=1028 * (3.28/5) '167 = 256* (3.28/5)
        'Scale_Factor = 1

        'Label3.Text = VRef 'Scale_Factor


        If CH1_Gain_RadioButton1.Checked = True Then '-12V to +12V
            Ch1_Gain = 1
        ElseIf CH1_Gain_RadioButton2.Checked = True Then '-6V to +6V
            Ch1_Gain = 2
        ElseIf CH1_Gain_RadioButton3.Checked = True Then '-2.4V to +2.4V
            Ch1_Gain = 5
        End If
        'Ch2
        If CH2_Gain_RadioButton1.Checked = True Then
            Ch2_Gain = 1
        ElseIf CH2_Gain_RadioButton2.Checked = True Then
            Ch2_Gain = 2
        ElseIf CH2_Gain_RadioButton3.Checked = True Then
            Ch2_Gain = 5
        End If

        Ch1_Offset = 1024 + CH1_Offset_VScrollBar.Value 'raw values
        Ch2_Offset = 1024 + CH2_Offset_VScrollBar.Value
        Ch1_Trig = 1024 + CH1_TrigLevel_VScrollBar.Value
        'Ch2_Trig = 1024 + CH2_TrigLevel_VScrollBar.Value

        Ch1_Offset1 = Ch1_Offset 'Corrected values
        Ch2_Offset1 = Ch2_Offset
        Ch1_Trig1 = Ch1_Trig
        Ch2_Trig1 = Ch1_Trig 'Ch2_Trig

        'write raw values to screen
        'keep Offset unchanged ,Trig Depends on Gain
        CH1_Offset_TextBox.Text = Math.Round((Ch1_Offset * 12 / 512 - 12), 2) * -1
        CH2_Offset_TextBox.Text = Math.Round((Ch2_Offset * 12 / 512 - 12), 2) * -1
        CH1_Trig_TextBox.Text = Math.Round((1 / Ch1_Gain) * (Ch1_Trig * 12 / 512 - 12), 2) * -1
        'CH2_Trig_TextBox.Text = Math.Round((1 / Ch2_Gain) * (Ch2_Trig * 12 / 512 - 12), 2) * -1

        'CH1_Offset_TextBox.Text = Math.Round((1 / Ch1_Gain) * (Ch1_Offset * 12 / 512 - 12), 2) * -1
        'CH2_Offset_TextBox.Text = Math.Round((1 / Ch2_Gain) * (Ch2_Offset * 12 / 512 - 12), 2) * -1
        'CH1_Trig_TextBox.Text = Math.Round((1 / Ch1_Gain) * (Ch1_Trig * 12 / 512 - 12), 2) * -1
        'CH2_Trig_TextBox.Text = Math.Round((1 / Ch2_Gain) * (Ch2_Trig * 12 / 512 - 12), 2) * -1

        'Based on the input resistor divider

        Offset_Gain_Factor = (1.29) / Scale_Factor '1.29 experimentally instead of 1.263
        'Offset_Gain_Factor = (48 / 38) / Scale_Factor
        'These are the Zero Offse Values
        Ch1_Offset1 = (512 * Scale_Factor * Offset_Gain_Factor) / Ch1_Gain
        Ch2_Offset1 = (512 * Scale_Factor * Offset_Gain_Factor) / Ch2_Gain

        'Additional offset to shift the display by the manual offset values 1024 is the max value

        Ch1_Offset1 = Ch1_Offset1 + (CH1_Offset_TextBox.Text / 12) * Offset_Gain_Factor * 512
        Ch2_Offset1 = Ch2_Offset1 + (CH2_Offset_TextBox.Text / 12) * Offset_Gain_Factor * 512


        If Ch1_Offset1 <= 0 Then
            Ch1_Offset1 = 0
        End If

        If Ch1_Offset1 >= 1024 Then
            Ch1_Offset1 = 1023
        End If
        If Ch2_Offset1 <= 0 Then
            Ch2_Offset1 = 0
        End If

        If Ch2_Offset1 >= 1024 Then
            Ch2_Offset1 = 1023
        End If
        'Offset_Correction in output values based on slider 12V scale
        OC1 = (CH1_Offset_TextBox.Text / 12) / (Scale_Factor * Offset_Gain_Factor)
        OC2 = (CH2_Offset_TextBox.Text / 12) / (Scale_Factor * Offset_Gain_Factor)

        'Trig level needs to be adjusted for Gain and shifted based on Main offsets

        Ch1_Trig1 = ((1024 - (Ch1_Trig1))) + OC1 * 4
        Ch2_Trig1 = ((1024 - (Ch2_Trig1))) + OC2 * 4

        'If Auto_Trig_RadioButton.Checked Then
        'Ch1_Trig1 = 0
        ' Ch2_Trig1 = 0
        ' End If
        ' If Ch1_Trig_RadioButton.Checked Then
        'Ch2_Trig1 = 0
        'End If
        'If CH2_Trig_RadioButton.Checked Then
        'Ch1_Trig1 = 0
        ' If

        'Dim SetCH1_Offset() As Byte = {&H4F, &H2, &H0} ' O msb-lsb
        'Dim SetCH2_Offset() As Byte = {&H6F, &H2, &H0} ' o msb-lsb
        'Dim SetTrigValue() As Byte = {&H4C, &H2, &H0} ' L msb-lsb

        'Convert to 2 Byte for writing to DAC's
        SetCH1_Offset(1) = Math.Floor(Ch1_Offset1 / 256)
        SetCH1_Offset(2) = Ch1_Offset1 - SetCH1_Offset(1) * 256

        SetTrigValue(1) = Math.Floor(Ch1_Trig1 / 256)
        SetTrigValue(2) = Ch1_Trig1 - SetTrigValue(1) * 256

        SetCH2_Offset(1) = Math.Floor(Ch2_Offset1 / 256)
        SetCH2_Offset(2) = Ch2_Offset1 - SetCH2_Offset(1) * 256

        'myAnalog_Trig2(2) = Math.Floor(Ch2_Trig1 / 256)
        'myAnalog_Trig2(3) = Ch2_Trig1 - myAnalog_Trig2(2) * 256

        mPort.Write(SetCH1_Offset, 0, 3)
        mPort.Write(SetCH2_Offset, 0, 3)
        mPort.Write(SetTrigValue, 0, 3)



    End Sub
#End Region
    'GAIN
#Region "Setup Gain"
    Private Sub Set_Gain_Bits()
        'Dim SetGainCmd() As Byte = {&H47, &H0, &H0} ' G CH1/CH2 Gains 1/2/5 0/1/3

        'Ch1
        If CH1_Gain_RadioButton1.Checked = True Then
            SetGainCmd(1) = &H0
        ElseIf CH1_Gain_RadioButton2.Checked = True Then
            SetGainCmd(1) = &H1
        ElseIf CH1_Gain_RadioButton3.Checked = True Then
            SetGainCmd(1) = &H3 '0000 0010
        End If
        'Ch2
        If CH2_Gain_RadioButton1.Checked = True Then
            SetGainCmd(2) = &H0
        ElseIf CH2_Gain_RadioButton2.Checked = True Then
            SetGainCmd(2) = &H1
        ElseIf CH2_Gain_RadioButton3.Checked = True Then
            SetGainCmd(2) = &H3
        End If

        mPort.Write(SetGainCmd, 0, 3)

    End Sub
#End Region
    'Trigger Source & Polarity
#Region "Setup Trigger Modes"
    Private Sub Set_Trigger_Mode()
        'Dim TrigSourceCmd() As Byte = {&H54, &H0} ' T Trig Source Auto/CH1/CH2 0/1/2
        If Auto_Trig_RadioButton().Checked = True Then
            TrigSourceCmd(1) = &H0 'Auto Mode
        ElseIf Ch1_Trig_RadioButton().Checked = True Then
            TrigSourceCmd(1) = &H1 'Trig By CH1
        Else
            TrigSourceCmd(1) = &H2
        End If

        mPort.Write(TrigSourceCmd, 0, 2)

        'Dim TrigPolarityCmd() As Byte = {&H50, &H0} ' P Trig Polarity L/H H/L 0/1
        'Trigger Edge H/L=1 ,L/H=0
        'L/H Default
        If HL_Trig_RadioButton().Checked = True Then
            TrigPolarityCmd(1) = &H1
        Else
        End If

        mPort.Write(TrigPolarityCmd, 0, 2)

    End Sub
#End Region
    ' 
#Region "Run"
    Private Sub Run_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Run_Button.Click
        '--mPort.DiscardInBuffer() 'Clear Input Buffer
        '--So that End of Busy "Done" is read correctly
        '--System.Threading.Thread.Sleep(50) 'sleep 30mSec
        '--mPort.Write(CaptureCmd, 0, 1)

        If Run_Button.Text <> "BUSY" Then 'Avoid problem when RUN button is pressed when Busy

            Opening_Panel.Hide() 'Hides the opening panel
            

            'Check for Capture method Single, Continuous, Storage
            If Single_RadioButton.Checked = True Then 'Single
                Keep_Running = False
                Overplot = False
            ElseIf Continuous_RadioButton.Checked = True Then 'Repeat
                Keep_Running = True 'Till Stopped
                Overplot = False
            ElseIf OverWrite_RadioButton.Checked = True Then ' Store
                Keep_Running = False 'Till Stopped
                Overplot = True
            End If

            'Set BUSY and Issue the SETUp & RUN Command
            Run_Button.Text = "BUSY"
            Stop_Button.Text = "STOP"

            Do
                If ETS_Error <> 1 Then 'Check not in ETS with Auto
                    Setup()
                    mPort.DiscardInBuffer() 'Clear Input Buffer
                    'Run Command
                    mPort.Write(CaptureCmd, 0, 1)



                    '-----------------------------
                    Application.DoEvents()
                    
                    '-------------------------------------------------

                    'Read_Data()

                    Read1()

                    'Check for DFT Modes
                    If DFT_CH1_Display_RadioButton.Checked() Or DFT_CH2_Display_RadioButton.Checked() Then
                        doDFT()
                    End If

                    'Call Plot()
                    Plot()
                End If
                
            Loop While Keep_Running 'based on Single, Continuous, Storage

            Stop_Button.Text = "DONE"
            Run_Button.Text = "RUN"

        End If
    End Sub

    'Stop Button
    Private Sub Stop_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Stop_Button.Click
        'mPort.Write(AbortCmd, 0, 1)
        Keep_Running = False
    End Sub
    'Select Mode Spare
    Private Sub Select_Mode()
        'Dim SetModeCmd() As Byte = {&H46, &H0} 'F 0/1/2 Ch1&Ch2 200 / Ch1 400/ Ch2 400


        If BothCh_Display_RadioButton.Checked = True Then
            SetModeCmd(1) = &H0 '200 Values each CH1 & CH2
            Data_Length = 200
            DFT = False
        ElseIf CH1_Display_RadioButton.Checked = True Then
            SetModeCmd(1) = &H1 '400 Values CH1
            Data_Length = 400
            DFT = False
        ElseIf CH2_Display_RadioButton.Checked = True Then
            SetModeCmd(1) = &H2 '400 Values CH2
            Data_Length = 400
            DFT = False
        ElseIf XY_Display_RadioButton.Checked = True Then
            SetModeCmd(1) = &H0 '200 Values each CH1 & CH2
            Data_Length = 200
            DFT = False
        ElseIf DFT_CH1_Display_RadioButton.Checked = True Then
            SetModeCmd(1) = &H1 '400 Values CH1
            Data_Length = 400
            DFT = True
        ElseIf DFT_CH2_Display_RadioButton.Checked = True Then
            SetModeCmd(1) = &H2 '400 Values CH1
            Data_Length = 400
            DFT = True
        End If
        mPort.Write(SetModeCmd, 0, 2)
    End Sub
    Private Sub Set_Sample_Rate()
        'Multiplier * data_length sets max x-axis time in Y vs Time mode
        Select Case Sample_Rate_ListBox.Text
            Case "20Mbps 0.05us/sample (ETS)"
                SetSampleRateCmd(1) = &H14 '20
                If Auto_Trig_RadioButton().Checked = True Then
                    Sample_Rate_ListBox.Text = "No ETS in Auto-Int"
                    MessageBox.Show("No ETS in Auto Mode")
                    ETS_Error = 1
                End If
                ETS = 1
                ETS_Error = 0
                Heading1 = "Time(uSec)"
                Multiplier = 1 / 20 '0.05
                Max_Frequency = 10000000
            Case "10Mbps 0.10us/sample (ETS)"
                SetSampleRateCmd(1) = &H13 '19
                If Auto_Trig_RadioButton().Checked = True Then
                    Sample_Rate_ListBox.Text = "No ETS in Auto-Int"
                    MessageBox.Show("No ETS in Auto Mode")
                    ETS_Error = 1
                End If
                ETS = 1
                ETS_Error = 0
                Heading1 = "Time(uSec)"
                Multiplier = 1 / 10 '0.1
                Max_Frequency = 5000000
            Case "5Mbps  0.20us/sample (ETS)"
                SetSampleRateCmd(1) = &H12 '18
                If Auto_Trig_RadioButton().Checked = True Then
                    Sample_Rate_ListBox.Text = "No ETS in Auto-Int"
                    MessageBox.Show("No ETS in Auto Mode")
                    ETS_Error = 1
                End If
                ETS = 1
                ETS_Error = 0
                Heading1 = "Time(uSec)"
                Multiplier = 1 / 5 '0.2
                Max_Frequency = 2500000
            Case "2Mbps  0.50us/sample (ETS)"
                SetSampleRateCmd(1) = &H11 '17
                If Auto_Trig_RadioButton().Checked = True Then
                    Sample_Rate_ListBox.Text = "No ETS in Auto-Int"
                    MessageBox.Show("No ETS in Auto Mode")
                    ETS_Error = 1
                End If
                ETS = 1
                ETS_Error = 0
                Heading1 = "Time(uSec)"
                Multiplier = 1 / 2 '0.5
                Max_Frequency = 1000000
            Case "1Mbps   1.0us/sample (ETS)"
                SetSampleRateCmd(1) = &H10 '16
                If Auto_Trig_RadioButton().Checked = True Then
                    Sample_Rate_ListBox.Text = "No ETS in Auto-Int"
                    MessageBox.Show("No ETS in Auto Mode")
                    ETS_Error = 1
                End If
                ETS = 1
                ETS_Error = 0
                Heading1 = "Time(uSec)"
                Multiplier = 1.0
                Max_Frequency = 500000
                '--------------------------------------------------------------------
            Case "500kbps 2.0us/sample (ETS)"
                SetSampleRateCmd(1) = 1

                ETS = 0
                ETS_Error = 0
                Heading1 = "Time(uSec)"
                Multiplier = 2.0
                Max_Frequency = 250000
            Case "200kbps 5.0us/sample (ETS)"
                SetSampleRateCmd(1) = 2

                ETS = 0
                ETS_Error = 0
                Heading1 = "Time(uSec)"
                Multiplier = 5
                Max_Frequency = 100000
            Case "100kbps  10us/sample"
                SetSampleRateCmd(1) = 3
                ETS = 0
                ETS_Error = 0
                Heading1 = "Time(uSec)"
                Multiplier = 10
                Max_Frequency = 50000
            Case "50kbps   20us/sample"
                SetSampleRateCmd(1) = 4
                ETS = 0
                ETS_Error = 0
                Heading1 = "Time(uSec)"
                Multiplier = 20
                Max_Frequency = 25000
            Case "20kbps   50us/sample"
                SetSampleRateCmd(1) = 5
                ETS = 0
                ETS_Error = 0
                Heading1 = "Time(uSec)"
                Multiplier = 50
                Max_Frequency = 10000
            Case "10kbps  100us/sample"
                SetSampleRateCmd(1) = 6
                ETS = 0
                ETS_Error = 0
                Heading1 = "Time(mSec)"
                Multiplier = 1 / 10 '0.1
                Max_Frequency = 5000
            Case "5kbps   200us/sample"
                SetSampleRateCmd(1) = 7
                ETS = 0
                ETS_Error = 0
                Heading1 = "Time(mSec)"
                Multiplier = 1 / 5 '0.2
                Max_Frequency = 2500
            Case "2kbps   500us/sample"
                SetSampleRateCmd(1) = 8
                ETS = 0
                ETS_Error = 0
                Heading1 = "Time(mSec)"
                Multiplier = 1 / 2 '0.5
                Max_Frequency = 1000
            Case "1kbps   1.0ms/sample"
                SetSampleRateCmd(1) = 9
                ETS = 0
                ETS_Error = 0
                Heading1 = "Time(mSec)"
                Multiplier = 1
                Max_Frequency = 500
            Case "500Hz   2.0ms/sample"
                SetSampleRateCmd(1) = &HA '10
                ETS = 0
                ETS_Error = 0
                Heading1 = "Time(mSec)"
                Multiplier = 2
                Max_Frequency = 250
            Case "200Hz   5.0ms/sample"
                SetSampleRateCmd(1) = &HB '11
                ETS = 0
                ETS_Error = 0
                Heading1 = "Time(mSec)"
                Multiplier = 5
                Max_Frequency = 100
            Case "100Hz    10ms/sample "
                SetSampleRateCmd(1) = &HC '12
                ETS = 0
                ETS_Error = 0
                Heading1 = "Time(mSec)"
                Multiplier = 10
                Max_Frequency = 50
            Case "50Hz     20ms/sample"
                SetSampleRateCmd(1) = 13
                ETS = 0
                ETS_Error = 0
                Heading1 = "Time(mSec)"
                Multiplier = 20
                Max_Frequency = 25
            Case "20Hz     50ms/sample"
                SetSampleRateCmd(1) = 14
                ETS = 0
                ETS_Error = 0
                Heading1 = "Time(mSec)"
                Multiplier = 50
                Max_Frequency = 10
            Case "10Hz    100ms/sample "
                SetSampleRateCmd(1) = 15
                ETS = 0
                ETS_Error = 0
                Heading1 = "Time(Sec)"
                Multiplier = 1 / 10 '0.1
                Max_Frequency = 5
        End Select

    End Sub
#End Region

#Region "Export CSV"
    Private Sub Export_CSV_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Export_CSV_Button.Click
        'Dim Heading1, Heading2, Heading3, Heading4 As String
        'Dim Multiplier As Integer
        'Dim Time_array(400), DFT_Array(400), Ch1_dataArray(400), Ch2_dataArray(400) As Integer
        'Dim DataArray(401, 4) As String
        DataArray(0, 0) = Heading1
        DataArray(0, 1) = "Ch1 Data"
        DataArray(0, 2) = "Ch2 Data"
        DataArray(0, 3) = "Frequency"
        DataArray(0, 4) = "Ch1 DFT"
        DataArray(0, 5) = "Ch2 DFT"
        'Creating the Time Array is done earlier
        If Run_Button.Text = "RUN" Then
            TwoDArrayToCSV(DataArray) 'Calling the Export CSV Routine got from Net
        End If
    End Sub


    Sub TwoDArrayToCSV(ByVal DataArray(,) As String)

        Dim str As String = ""
        Dim ofile As String = ""

        svDialog("csv|*.csv", "save as...", ofile)
        Dim sw As System.IO.StreamWriter = New System.IO.StreamWriter(ofile)

        For i As Int32 = DataArray.GetLowerBound(0) To DataArray.GetUpperBound(0)
            For j As Int32 = DataArray.GetLowerBound(1) To DataArray.GetUpperBound(1)
                str += DataArray(i, j) + ","
            Next
            sw.WriteLine(str)
            str = ""
        Next
        sw.Flush()
        sw.Close()
    End Sub

    Sub svDialog(ByVal infilter As String, ByVal dtitle As String, ByRef outfile As String)
        Dim openFileDialog1 As New SaveFileDialog()
        With openFileDialog1
            .Filter = infilter
            .FilterIndex = 1
            .Title = dtitle
            .DefaultExt = Strings.Right(infilter, 3)
            .ShowDialog()
            outfile = openFileDialog1.FileName
            .RestoreDirectory = True
        End With
    End Sub
#End Region

#Region "Plot"
    'Private Sub Plot_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Plot_Button.Click
    Private Sub Plot()
        Dim myPane As GraphPane = Aj_ZG1.GraphPane
        Dim rowno As Integer
        Dim Symbol As Boolean = False

        Dim list As New PointPairList
        Dim list2 As New PointPairList
        Dim x, y, y2 As String 'Double

        'Dim display_y1, display_y2, display_xy, display_dft1, display_dft2 As Boolean
        'Dim DataArray(401, 4) As String
        'Dim xcolumn, ycolumn, y1column As Integer
        'Check what_to_plot

        'what_to_plot"

        'Proceed with data processing Plotting only if no null values are detected
        'Populate the List assume no null values
        'If Plot_Error = False Then


        If BothCh_Display_RadioButton.Checked Then
            xcolumn = 0
            ycolumn = 1
            y1column = 2
            myPane.YAxis.Title.Text = "Ch1 Volts"
            myPane.Y2Axis.Title.Text = "Ch2 Volts"
            myPane.XAxis.Title.Text = Heading1
            myPane.YAxis.IsVisible = True
            myPane.Y2Axis.IsVisible = True
            Sq_Display = False
            DFT_Display = False
            For rowno = 1 To (Data_Length)
                x = DataArray(rowno, 0)
                y = DataArray(rowno, 1)
                list.Add(x, y)
                y2 = DataArray(rowno, 2)
                list2.Add(x, y2)
            Next
            myPane.YAxis.Scale.Min = -12 / Ch1_Gain
            myPane.YAxis.Scale.Max = 12 / Ch1_Gain
            myPane.Y2Axis.Scale.Min = -12 / Ch2_Gain
            myPane.Y2Axis.Scale.Max = 12 / Ch2_Gain
            myPane.XAxis.Scale.Min = 0
            myPane.XAxis.Scale.Max = Data_Length * Multiplier
        ElseIf CH1_Display_RadioButton.Checked() Then
            xcolumn = 0
            ycolumn = 1
            myPane.YAxis.Title.Text = "Ch1 Volts"
            myPane.XAxis.Title.Text = Heading1
            myPane.YAxis.IsVisible = True
            myPane.Y2Axis.IsVisible = False
            Sq_Display = False
            DFT_Display = False
            display_y1 = False
            For rowno = 1 To (Data_Length)
                x = DataArray(rowno, 0)
                y = DataArray(rowno, 1)
                list.Add(x, y)
            Next
            myPane.YAxis.Scale.Min = -12 / Ch1_Gain
            myPane.YAxis.Scale.Max = 12 / Ch1_Gain
            myPane.XAxis.Scale.Min = 0
            myPane.XAxis.Scale.Max = Data_Length * Multiplier
        ElseIf CH2_Display_RadioButton.Checked() Then
            xcolumn = 0
            ycolumn = 2
            myPane.YAxis.Title.Text = "Ch2 Volts"
            myPane.XAxis.Title.Text = Heading1
            myPane.YAxis.IsVisible = True
            myPane.Y2Axis.IsVisible = False
            Sq_Display = False
            DFT_Display = False
            For rowno = 1 To (Data_Length)
                x = DataArray(rowno, 0)
                y = DataArray(rowno, 2)
                list.Add(x, y)
            Next
            myPane.YAxis.Scale.Min = -12 / Ch2_Gain
            myPane.YAxis.Scale.Max = 12 / Ch2_Gain
            myPane.XAxis.Scale.Min = 0
            myPane.XAxis.Scale.Max = Data_Length * Multiplier
        ElseIf XY_Display_RadioButton.Checked() Then
            xcolumn = 1
            ycolumn = 2
            Sq_Display = True
            myPane.XAxis.Title.Text = "Ch1 Volts"
            myPane.YAxis.Title.Text = "Ch2 Volts"
            myPane.YAxis.IsVisible = True
            myPane.Y2Axis.IsVisible = False
            Sq_Display = True
            DFT_Display = False
            For rowno = 1 To (Data_Length)
                x = DataArray(rowno, 1)
                y = DataArray(rowno, 2)
                list.Add(x, y)
            Next
        ElseIf DFT_CH1_Display_RadioButton.Checked() Then
            xcolumn = 3
            ycolumn = 4
            myPane.YAxis.Title.Text = "Ch1 Volts"
            myPane.XAxis.Title.Text = "Frequency"
            myPane.YAxis.IsVisible = True
            myPane.Y2Axis.IsVisible = False
            Sq_Display = False
            DFT_Display = True
            For rowno = 1 To (Data_Length)
                x = DataArray(rowno, 3)
                y = DataArray(rowno, 4)
                list.Add(x, y)
            Next
        ElseIf DFT_CH2_Display_RadioButton.Checked() Then
            xcolumn = 3
            ycolumn = 5
            myPane.YAxis.Title.Text = "Ch1 Volts"
            myPane.XAxis.Title.Text = "Frequency"
            myPane.YAxis.IsVisible = True
            myPane.Y2Axis.IsVisible = False
            DFT_Display = True
            Sq_Display = False
            For rowno = 1 To (Data_Length)
                x = DataArray(rowno, 3)
                y = DataArray(rowno, 5)
                list.Add(x, y)
            Next
        End If
        'End of what to plot


        'Clear Previous Graph if Over Plot is Not Checked
        'Overplot = True
        If Overplot = True Then
        Else
            Aj_ZG1.GraphPane.CurveList.Clear()
            Aj_ZG1.GraphPane.GraphObjList.Clear()
        End If

        ' Set the Title
        myPane.Title.Text = Graph_Title_TextBox.Text '"Aj Test Plot" 'Title_TextBox.Text

        'Proceed with data processing Plotting only if no null values are detected
        'Populate the List assume no null values


        ' Generate a red curve with diamond symbols, and "Alpha" in the legend
        Dim myCurve As LineItem

        If Symbol = True Then
            myCurve = myPane.AddCurve(" ", list, Color.Red, SymbolType.Diamond)
            ' Fill the symbols with white
            myCurve.Symbol.Fill = New Fill(Color.White)
        Else
            myCurve = myPane.AddCurve(" ", list, Color.Red, SymbolType.None) 'No Symbols
        End If

        ' Generate a blue curve with circle symbols, and "Beta" in the legend

        If Symbol = True Then
            myCurve = myPane.AddCurve(" ", list2, Color.Blue, SymbolType.Circle)
            ' Fill the symbols with white
            myCurve.Symbol.Fill = New Fill(Color.White)
            ' Associate this curve with the Y2 axis
            myCurve.IsY2Axis = True
        Else
            myCurve = myPane.AddCurve(" ", list2, Color.Blue, SymbolType.None) 'No Symbols
            ' Associate this curve with the Y2 axis
            myCurve.IsY2Axis = True
        End If


        'Do not show the legends
        myPane.Legend.IsVisible = False
        ' Show the x axis grid
        myPane.XAxis.MajorGrid.IsVisible = True

        ' Make the Y axis scale red
        myPane.YAxis.Scale.FontSpec.FontColor = Color.Red
        myPane.YAxis.Title.FontSpec.FontColor = Color.Red
        ' turn off the opposite tics so the Y tics don't show up on the Y2 axis
        myPane.YAxis.MajorTic.IsOpposite = False
        myPane.YAxis.MinorTic.IsOpposite = False
        ' Don't display the Y zero line
        myPane.YAxis.MajorGrid.IsZeroLine = False
        ' Align the Y axis labels so they are flush to the axis
        myPane.YAxis.Scale.Align = AlignP.Inside
        ' Display the Y2 axis grid lines
        myPane.YAxis.MajorGrid.IsVisible = True

        ' Manually set the axis range

        If Sq_Display = True Then  ' XY Mode
            myPane.YAxis.Scale.Min = -12 / Ch1_Gain
            myPane.YAxis.Scale.Max = 12 / Ch1_Gain
            myPane.XAxis.Scale.Min = -12 / Ch2_Gain
            myPane.XAxis.Scale.Max = 12 / Ch2_Gain
            myPane.YAxis.MajorGrid.IsVisible = True
        ElseIf DFT_Display = True Then  'DFT Mode
            myPane.YAxis.Scale.Min = 0
            myPane.YAxis.Scale.Max = Max_DFT
            myPane.XAxis.Scale.Min = 0
            myPane.XAxis.Scale.Max = Max_Frequency * 1 / 2
        End If

        'Enable the Y and Y2 axis display
        'myPane.YAxis.IsVisible = True


        ' Enable the Y2 axis display
        'myPane.Y2Axis.IsVisible = True
        'myPane.Y2Axis.Scale.Min = -12
        'myPane.Y2Axis.Scale.Max = 12

        ' Make the Y2 axis scale blue
        myPane.Y2Axis.Scale.FontSpec.FontColor = Color.Blue
        myPane.Y2Axis.Title.FontSpec.FontColor = Color.Blue
        ' turn off the opposite tics so the Y2 tics don't show up on the Y axis
        myPane.Y2Axis.MajorTic.IsOpposite = False
        myPane.Y2Axis.MinorTic.IsOpposite = False
        ' Display the Y2 axis grid lines
        myPane.Y2Axis.MajorGrid.IsVisible = True
        ' Align the Y2 axis labels so they are flush to the axis
        myPane.Y2Axis.Scale.Align = AlignP.Inside


        ' Fill the axis background with a gradient
        myPane.Chart.Fill = New Fill(Color.White, Color.LightGray, 45.0F)

        ' Add a text box with Aj-Scope Signature
        'Increaseing First 0.02F moves to Right , Reducing second 0.9F moves UP
        Dim mytext As New TextObj("Aj-" _
        & "S" & "c" & "o" & "p" & "e", 0.85F, 0.95F, _
        CoordType.ChartFraction, AlignH.Left, AlignV.Bottom)
        'mytext.FontSpec.StringAlignment = StringAlignment.Near
        myPane.GraphObjList.Add(mytext)



        ' Add a text box with instructions
        '
        'Dim text As New TextObj( _
        '"Zoom: left mouse & drag" & Chr(10) & "Pan: middle mouse & drag" & Chr(10) _
        '& "Context Menu: right mouse", _
        '0.05F, 0.95F, CoordType.ChartFraction, AlignH.Left, AlignV.Bottom)
        'text.FontSpec.StringAlignment = StringAlignment.Near
        'myPane.GraphObjList.Add(text)

        ' Enable scrollbars if needed
        Aj_ZG1.IsShowHScrollBar = True
        Aj_ZG1.IsShowVScrollBar = True
        Aj_ZG1.IsAutoScrollRange = True
        If display_y1 = False Then 'Y2 Not Required
            Aj_ZG1.IsScrollY2 = True
        End If
        Aj_ZG1.IsShowPointValues = True

        ' Size the control to fit the window
        'SetSize()

        ' Tell ZedGraph to calculate the axis ranges
        ' Note that you MUST call this after enabling IsAutoScrollRange, 
        'since AxisChange() sets
        ' up the proper scrolling parameters
        Aj_ZG1.AxisChange()
        ' Make sure the Graph gets redrawn
        Aj_ZG1.Invalidate()

        ' Size the control to fit the window
        SetSize()


        'Not checking
        'MessageBox.Show("Null Entries Set To 0 - Exiting")

    End Sub
    Private Sub Aj_ZG1_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs)
        SetSize()
    End Sub
    Private Sub SetSize()
        Dim loc As New Point(0, 140)
        'Aj_ZG1.Location = loc()
        ' Leave a small margin around the outside of the control
        Dim size1 As New Size(Me.ClientRectangle.Width - Me.ClientRectangle.Width * 350 / 800, Me.ClientRectangle.Height - Me.ClientRectangle.Height * 140 / 600) 'Rectangle
        Dim size2 As New Size(Me.ClientRectangle.Height - Me.ClientRectangle.Height * 140 / 600, Me.ClientRectangle.Height - Me.ClientRectangle.Height * 140 / 600) 'Square

        'Dim size As New Size(Me.ClientRectangle.Height - 50, Me.ClientRectangle.Height - 50)
        If Sq_Display = False Then 'Choose depending on time or XY, DFT
            Aj_ZG1.Size = size1 'Rectangular
        Else
            Aj_ZG1.Size = size2 ' Square
        End If

    End Sub

#End Region

#Region "DFT"
    Private Sub DFT_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        doDFT()
    End Sub
    Public Sub doDFT()
        Dim cnt, N, N1, k, i, DFT_column As Integer
        Dim j, rowno, gain As Integer

        If DFT_CH1_Display_RadioButton.Checked() Then
            ycolumn = 1
            DFT_column = 4 'DFT CH1
            gain = 1 'Ch1_Gain
        ElseIf DFT_CH2_Display_RadioButton.Checked() Then
            ycolumn = 2
            DFT_column = 5 'DFT CH2
            gain = 1 'Ch2_Gain
        End If
        'x = dataArray(rowno, xcolumn)

        'THE DISCRETE FOURIER TRANSFORM
        'copyright © 1997-1999 by California Technical Publishing
        'published with  permission from Steven W Smith, www.dspguide.com

        Const PI = 3.14159265
        N = Data_Length * 2 'appending 0's to increase resolution
        N1 = Data_Length
        Dim REX(N / 2 + 1)
        Dim IMX(N / 2 + 1)
        Dim dataArrayDFT(800) As String
        Dim outputArray(N / 2 + 1)
        Dim maxout_temp As Double
        Dim maxout As String


        'Initialise dataArray
        For j = 0 To 800
            dataArrayDFT(j) = 0
        Next
        'transfer Ain1 values to dataArray
        j = 0
        For rowno = 0 To N1 - 1
            dataArrayDFT(j) = DataArray(rowno + 1, ycolumn)
            j = j + 1
        Next


        For k = 0 To UBound(REX) - 1
            REX(k) = 0
            IMX(k) = 0
            For i = 1 To N
                REX(k) = REX(k) + dataArrayDFT(i) * Cos(2 * PI * k * i / N)
                IMX(k) = IMX(k) - dataArrayDFT(i) * Sin(2 * PI * k * i / N)

            Next i
        Next k

        maxout = 0
        For cnt = 1 To UBound(outputArray) - 1
            outputArray(cnt) = (IMX(cnt) * IMX(cnt)) + (REX(cnt) * REX(cnt))
            outputArray(cnt) = Sqrt(outputArray(cnt))
            If outputArray(cnt) > maxout Then
                maxout = outputArray(cnt)
            End If
        Next cnt
        maxout = (maxout * 2 / Data_Length) / gain
        'Transfer outputArray to DFT column Scale to Max 10
        j = 0
        For rowno = 0 To (N1 / 2 - 1)
            DataArray(rowno + 1, DFT_column) = Math.Round(((outputArray(j + 1)) * 2 / Data_Length) / gain, 4)
            j = j + 1
        Next
        'maxout = (maxout * 2 / Data_Length) / gain
        'maxout = Math.Round(maxout_temp, 2)
        maxout_temp = maxout
        maxout_temp = Math.Round(maxout_temp, 4)
        'DFT1_Max_TextBox.Text = maxout
        Max_DFT = maxout_temp + 0.1

    End Sub



#End Region
#Region "EXIT"
    Private Sub Exit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EXIT_Button.Click
        Me.Close()
    End Sub
#End Region



    'New Parts 
#Region "New Parts"

    Private Sub TestLEDButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TestLEDButton.Click
        mPort.Write(TestLEDcmd, 0, 1)
    End Sub

#End Region
    'Private Sub ReadDataButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReadDataButton.Click
#Region "Read1"

    Private Sub Read1()
        'Ch1_dataArray(400), Ch2_dataArray(400)

        Dim read_temp, read_temp1 As Double
        Dim rowno As Integer
        Dim MaxFreq_by_DataLength As String
        Dim Ack_array(5) As Byte

        ' Data_Length = 200, 400 setup earlier

        'Capture Command is given before this expecting "Done"
        System.Threading.Thread.Sleep(50) 'sleep 30mSec
        'Wait for capture complete, system sends "Done"
        While (mPort.BytesToRead = 0)
        End While
        mPort.Read(Ack_array, 0, 4)

        mPort.DiscardInBuffer() 'Clear Input Buffer
        'Issue Read Command for CH1 data
        If Data_Length = 200 Then
            ReadDataCmd(1) = &H1 '200 CH1
        Else
            ReadDataCmd(1) = &H3 '400 CH1 / CH2
        End If
        mPort.Write(ReadDataCmd, 0, 2) 'CH1 Data
        'Sleep a bit
        System.Threading.Thread.Sleep(50) 'sleep 30mSec
        'Wait for data and read it 
        While (mPort.BytesToRead = 0)
        End While
        mPort.Read(Ch1_dataArray, 0, Data_Length) ' Read Serial data
        System.Threading.Thread.Sleep(150) 'sleep 30mSec

        mPort.DiscardInBuffer() 'Clear Input Buffer
        'Issue Read Command for CH2 data
        If Data_Length = 200 Then
            ReadDataCmd(1) = &H2 '200 CH2
        Else
            ReadDataCmd(1) = &H3 '400 CH1 / CH2
        End If

        mPort.Write(ReadDataCmd, 0, 2) 'CH1 Data
        'Sleep a bit
        System.Threading.Thread.Sleep(50) 'sleep 30mSec
        'Wait for data and read it 
        While (mPort.BytesToRead = 0)
        End While
        mPort.Read(Ch2_dataArray, 0, Data_Length) ' Read Serial data

        'Read Again if required

        'Scale and Move data to  Dim DataArray(401, 4) As String
        'rowno (0) = headers
        'CH1 data
        For rowno = 1 To Data_Length
            read_temp = Ch1_dataArray(rowno - 1) * 12 / 128 - 12
            read_temp = read_temp + OC1 'add shift_offset in 12V range before scaling
            read_temp = (read_temp * Scale_Factor) / Ch1_Gain
            read_temp = read_temp + OC1
            'For 8 Bit data -20V corresponds to 0 and +12 to 255 modified by Gain
            read_temp1 = Math.Round(read_temp, 2)
            DataArray(rowno, 1) = read_temp1
        Next
        'CH2 data
        For rowno = 1 To Data_Length
            read_temp = Ch2_dataArray(rowno - 1) * 12 / 128 - 12
            read_temp = (read_temp * Scale_Factor) / Ch2_Gain
            'For 8 Bit data -10V corresponds to 0 and +12 to 255 modified by Gain
            read_temp1 = Math.Round(read_temp, 2)
            DataArray(rowno, 2) = read_temp1
        Next
        'Time/Sample anf Frequency 
        MaxFreq_by_DataLength = (Max_Frequency / (Data_Length))
        For rowno = 1 To Data_Length
            DataArray(rowno, 0) = (rowno - 1) * Multiplier 'Time per sample
            DataArray(rowno - 1, 3) = (rowno - 1) * MaxFreq_by_DataLength 'Freq/sample
            'Because additional 0's are appended
        Next
        'End Sub


    End Sub
#End Region

    Private Sub PlotButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        'Private Sub Plot1()
        If DFT = True Then
            doDFT()
        End If
        Overplot = False
        Sq_Display = False
        Plot()

        SetSize()
    End Sub

    Private Sub DFT_Button_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        doDFT()
    End Sub
#Region "SETUP"
    'Private Sub RUN1Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SETUP_Button.Click
    Private Sub Setup()
        Set_Trigger_Mode() '"T" Sets up Trig Source & "P" Polarity
        System.Threading.Thread.Sleep(5) 'sleep 50mSec
        Select_Mode() ' "F" Sets up Both/CH1/CH2, Data_Length & DFT True/False
        System.Threading.Thread.Sleep(5) 'sleep 50mSec
        Compute_Offset_Trigger() '"R" "O" "o" "L" Reads Vref, Computes Scale-Factor, 
        '-------------------------Sets Offsets and Trig level
        System.Threading.Thread.Sleep(5) 'sleep 50mSec
        Set_Gain_Bits() ' "G" sets CH1/CH2 gains 
        System.Threading.Thread.Sleep(5) 'sleep 50mSec
        Set_Sample_Rate() ' "S" sets sampling rate 
        System.Threading.Thread.Sleep(5) 'sleep 50mSec
        mPort.Write(SetSampleRateCmd, 0, 2)

    End Sub
#End Region

End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Scope_Main_Form
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Scope_Main_Form))
        Me.Connect_GroupBox = New System.Windows.Forms.GroupBox
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.Test_TextBox = New System.Windows.Forms.TextBox
        Me.Com_Ports = New System.Windows.Forms.ListBox
        Me.Get_Ports_Button = New System.Windows.Forms.Button
        Me.Mode_GroupBox = New System.Windows.Forms.GroupBox
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.HL_Trig_RadioButton = New System.Windows.Forms.RadioButton
        Me.LH_Trig_RadioButton = New System.Windows.Forms.RadioButton
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.CH2_Trig_RadioButton = New System.Windows.Forms.RadioButton
        Me.Ch1_Trig_RadioButton = New System.Windows.Forms.RadioButton
        Me.Auto_Trig_RadioButton = New System.Windows.Forms.RadioButton
        Me.Display_GroupBox = New System.Windows.Forms.GroupBox
        Me.DFT_CH2_Display_RadioButton = New System.Windows.Forms.RadioButton
        Me.BothCh_Display_RadioButton = New System.Windows.Forms.RadioButton
        Me.DFT_CH1_Display_RadioButton = New System.Windows.Forms.RadioButton
        Me.XY_Display_RadioButton = New System.Windows.Forms.RadioButton
        Me.CH1_Display_RadioButton = New System.Windows.Forms.RadioButton
        Me.CH2_Display_RadioButton = New System.Windows.Forms.RadioButton
        Me.CH1_Gain_GroupBox = New System.Windows.Forms.GroupBox
        Me.CH1_Gain_RadioButton1 = New System.Windows.Forms.RadioButton
        Me.CH1_Gain_RadioButton2 = New System.Windows.Forms.RadioButton
        Me.CH1_Gain_RadioButton3 = New System.Windows.Forms.RadioButton
        Me.CH2_Gain_GroupBox = New System.Windows.Forms.GroupBox
        Me.CH2_Gain_RadioButton1 = New System.Windows.Forms.RadioButton
        Me.CH2_Gain_RadioButton2 = New System.Windows.Forms.RadioButton
        Me.CH2_Gain_RadioButton3 = New System.Windows.Forms.RadioButton
        Me.Run_Button = New System.Windows.Forms.Button
        Me.Export_CSV_Button = New System.Windows.Forms.Button
        Me.CH2_Offset_VScrollBar = New System.Windows.Forms.VScrollBar
        Me.CH1_Offset_VScrollBar = New System.Windows.Forms.VScrollBar
        Me.C = New System.Windows.Forms.GroupBox
        Me.CH2_Offset_TextBox = New System.Windows.Forms.TextBox
        Me.CH1_Offset_TextBox = New System.Windows.Forms.TextBox
        Me.CH2_offset_Value_Label = New System.Windows.Forms.Label
        Me.CH1_offset_Value_Label = New System.Windows.Forms.Label
        Me.CH2_Label = New System.Windows.Forms.Label
        Me.CH1_Label = New System.Windows.Forms.Label
        Me.Trig_Level_GroupBox = New System.Windows.Forms.GroupBox
        Me.CH1_Trig_TextBox = New System.Windows.Forms.TextBox
        Me.CH1_Trig_Level_Label = New System.Windows.Forms.Label
        Me.CH1_TrigLevel_VScrollBar = New System.Windows.Forms.VScrollBar
        Me.Label2 = New System.Windows.Forms.Label
        Me.Capture_Mode_GroupBox = New System.Windows.Forms.GroupBox
        Me.Single_RadioButton = New System.Windows.Forms.RadioButton
        Me.Continuous_RadioButton = New System.Windows.Forms.RadioButton
        Me.OverWrite_RadioButton = New System.Windows.Forms.RadioButton
        Me.Number_of_Samples_GroupBox = New System.Windows.Forms.GroupBox
        Me.Samples_RadioButton1 = New System.Windows.Forms.RadioButton
        Me.Samples_RadioButton2 = New System.Windows.Forms.RadioButton
        Me.Samples_RadioButton3 = New System.Windows.Forms.RadioButton
        Me.Set_Sample_RateGroupBox = New System.Windows.Forms.GroupBox
        Me.Sample_Rate_ListBox = New System.Windows.Forms.ListBox
        Me.Stop_Button = New System.Windows.Forms.Button
        Me.Graph_Title_TextBox = New System.Windows.Forms.TextBox
        Me.EXIT_Button = New System.Windows.Forms.Button
        Me.Aj_ZG1 = New ZedGraph.ZedGraphControl
        Me.Opening_Panel = New System.Windows.Forms.Panel
        Me.RichTextBox = New System.Windows.Forms.RichTextBox
        Me.TestLEDButton = New System.Windows.Forms.Button
        Me.SETUP_Button = New System.Windows.Forms.Button
        Me.Connect_GroupBox.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Mode_GroupBox.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Display_GroupBox.SuspendLayout()
        Me.CH1_Gain_GroupBox.SuspendLayout()
        Me.CH2_Gain_GroupBox.SuspendLayout()
        Me.C.SuspendLayout()
        Me.Trig_Level_GroupBox.SuspendLayout()
        Me.Capture_Mode_GroupBox.SuspendLayout()
        Me.Number_of_Samples_GroupBox.SuspendLayout()
        Me.Set_Sample_RateGroupBox.SuspendLayout()
        Me.Opening_Panel.SuspendLayout()
        Me.SuspendLayout()
        '
        'Connect_GroupBox
        '
        Me.Connect_GroupBox.Controls.Add(Me.Panel5)
        Me.Connect_GroupBox.Controls.Add(Me.Com_Ports)
        Me.Connect_GroupBox.Controls.Add(Me.Get_Ports_Button)
        Me.Connect_GroupBox.Location = New System.Drawing.Point(15, 12)
        Me.Connect_GroupBox.Name = "Connect_GroupBox"
        Me.Connect_GroupBox.Size = New System.Drawing.Size(310, 54)
        Me.Connect_GroupBox.TabIndex = 0
        Me.Connect_GroupBox.TabStop = False
        Me.Connect_GroupBox.Text = "Connect"
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.SystemColors.Window
        Me.Panel5.Controls.Add(Me.Test_TextBox)
        Me.Panel5.Location = New System.Drawing.Point(173, 15)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(120, 30)
        Me.Panel5.TabIndex = 20
        '
        'Test_TextBox
        '
        Me.Test_TextBox.Location = New System.Drawing.Point(7, 4)
        Me.Test_TextBox.Name = "Test_TextBox"
        Me.Test_TextBox.Size = New System.Drawing.Size(110, 20)
        Me.Test_TextBox.TabIndex = 26
        Me.Test_TextBox.Text = " Status Message"
        '
        'Com_Ports
        '
        Me.Com_Ports.FormattingEnabled = True
        Me.Com_Ports.Location = New System.Drawing.Point(97, 15)
        Me.Com_Ports.Name = "Com_Ports"
        Me.Com_Ports.Size = New System.Drawing.Size(70, 30)
        Me.Com_Ports.TabIndex = 5
        '
        'Get_Ports_Button
        '
        Me.Get_Ports_Button.Location = New System.Drawing.Point(6, 15)
        Me.Get_Ports_Button.Name = "Get_Ports_Button"
        Me.Get_Ports_Button.Size = New System.Drawing.Size(85, 30)
        Me.Get_Ports_Button.TabIndex = 4
        Me.Get_Ports_Button.Text = "Check Ports"
        Me.Get_Ports_Button.UseVisualStyleBackColor = True
        '
        'Mode_GroupBox
        '
        Me.Mode_GroupBox.Controls.Add(Me.Panel3)
        Me.Mode_GroupBox.Controls.Add(Me.Panel2)
        Me.Mode_GroupBox.Location = New System.Drawing.Point(748, 12)
        Me.Mode_GroupBox.Name = "Mode_GroupBox"
        Me.Mode_GroupBox.Size = New System.Drawing.Size(260, 96)
        Me.Mode_GroupBox.TabIndex = 1
        Me.Mode_GroupBox.TabStop = False
        Me.Mode_GroupBox.Text = "Trigger Mode"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.HL_Trig_RadioButton)
        Me.Panel3.Controls.Add(Me.LH_Trig_RadioButton)
        Me.Panel3.Location = New System.Drawing.Point(133, 20)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(58, 48)
        Me.Panel3.TabIndex = 8
        '
        'HL_Trig_RadioButton
        '
        Me.HL_Trig_RadioButton.AutoSize = True
        Me.HL_Trig_RadioButton.Location = New System.Drawing.Point(3, 26)
        Me.HL_Trig_RadioButton.Name = "HL_Trig_RadioButton"
        Me.HL_Trig_RadioButton.Size = New System.Drawing.Size(44, 17)
        Me.HL_Trig_RadioButton.TabIndex = 4
        Me.HL_Trig_RadioButton.Text = "H/L"
        Me.HL_Trig_RadioButton.UseVisualStyleBackColor = True
        '
        'LH_Trig_RadioButton
        '
        Me.LH_Trig_RadioButton.AutoSize = True
        Me.LH_Trig_RadioButton.Checked = True
        Me.LH_Trig_RadioButton.Location = New System.Drawing.Point(3, 3)
        Me.LH_Trig_RadioButton.Name = "LH_Trig_RadioButton"
        Me.LH_Trig_RadioButton.Size = New System.Drawing.Size(44, 17)
        Me.LH_Trig_RadioButton.TabIndex = 3
        Me.LH_Trig_RadioButton.TabStop = True
        Me.LH_Trig_RadioButton.Text = "L/H"
        Me.LH_Trig_RadioButton.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.CH2_Trig_RadioButton)
        Me.Panel2.Controls.Add(Me.Ch1_Trig_RadioButton)
        Me.Panel2.Controls.Add(Me.Auto_Trig_RadioButton)
        Me.Panel2.Location = New System.Drawing.Point(53, 20)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(55, 71)
        Me.Panel2.TabIndex = 7
        '
        'CH2_Trig_RadioButton
        '
        Me.CH2_Trig_RadioButton.AutoSize = True
        Me.CH2_Trig_RadioButton.Location = New System.Drawing.Point(6, 49)
        Me.CH2_Trig_RadioButton.Name = "CH2_Trig_RadioButton"
        Me.CH2_Trig_RadioButton.Size = New System.Drawing.Size(46, 17)
        Me.CH2_Trig_RadioButton.TabIndex = 2
        Me.CH2_Trig_RadioButton.Text = "CH2"
        Me.CH2_Trig_RadioButton.UseVisualStyleBackColor = True
        '
        'Ch1_Trig_RadioButton
        '
        Me.Ch1_Trig_RadioButton.AutoSize = True
        Me.Ch1_Trig_RadioButton.Location = New System.Drawing.Point(6, 26)
        Me.Ch1_Trig_RadioButton.Name = "Ch1_Trig_RadioButton"
        Me.Ch1_Trig_RadioButton.Size = New System.Drawing.Size(46, 17)
        Me.Ch1_Trig_RadioButton.TabIndex = 1
        Me.Ch1_Trig_RadioButton.Text = "CH1"
        Me.Ch1_Trig_RadioButton.UseVisualStyleBackColor = True
        '
        'Auto_Trig_RadioButton
        '
        Me.Auto_Trig_RadioButton.AutoSize = True
        Me.Auto_Trig_RadioButton.Checked = True
        Me.Auto_Trig_RadioButton.Location = New System.Drawing.Point(6, 3)
        Me.Auto_Trig_RadioButton.Name = "Auto_Trig_RadioButton"
        Me.Auto_Trig_RadioButton.Size = New System.Drawing.Size(47, 17)
        Me.Auto_Trig_RadioButton.TabIndex = 0
        Me.Auto_Trig_RadioButton.TabStop = True
        Me.Auto_Trig_RadioButton.Text = "Auto"
        Me.Auto_Trig_RadioButton.UseVisualStyleBackColor = True
        '
        'Display_GroupBox
        '
        Me.Display_GroupBox.Controls.Add(Me.DFT_CH2_Display_RadioButton)
        Me.Display_GroupBox.Controls.Add(Me.BothCh_Display_RadioButton)
        Me.Display_GroupBox.Controls.Add(Me.DFT_CH1_Display_RadioButton)
        Me.Display_GroupBox.Controls.Add(Me.XY_Display_RadioButton)
        Me.Display_GroupBox.Controls.Add(Me.CH1_Display_RadioButton)
        Me.Display_GroupBox.Controls.Add(Me.CH2_Display_RadioButton)
        Me.Display_GroupBox.Location = New System.Drawing.Point(328, 12)
        Me.Display_GroupBox.Name = "Display_GroupBox"
        Me.Display_GroupBox.Size = New System.Drawing.Size(402, 45)
        Me.Display_GroupBox.TabIndex = 2
        Me.Display_GroupBox.TabStop = False
        Me.Display_GroupBox.Text = "Display Mode"
        '
        'DFT_CH2_Display_RadioButton
        '
        Me.DFT_CH2_Display_RadioButton.AutoSize = True
        Me.DFT_CH2_Display_RadioButton.Location = New System.Drawing.Point(316, 23)
        Me.DFT_CH2_Display_RadioButton.Name = "DFT_CH2_Display_RadioButton"
        Me.DFT_CH2_Display_RadioButton.Size = New System.Drawing.Size(70, 17)
        Me.DFT_CH2_Display_RadioButton.TabIndex = 7
        Me.DFT_CH2_Display_RadioButton.Text = "DFT CH2"
        Me.DFT_CH2_Display_RadioButton.UseVisualStyleBackColor = True
        '
        'BothCh_Display_RadioButton
        '
        Me.BothCh_Display_RadioButton.AutoSize = True
        Me.BothCh_Display_RadioButton.Checked = True
        Me.BothCh_Display_RadioButton.Location = New System.Drawing.Point(3, 23)
        Me.BothCh_Display_RadioButton.Name = "BothCh_Display_RadioButton"
        Me.BothCh_Display_RadioButton.Size = New System.Drawing.Size(79, 17)
        Me.BothCh_Display_RadioButton.TabIndex = 6
        Me.BothCh_Display_RadioButton.TabStop = True
        Me.BothCh_Display_RadioButton.Text = "CH1 && CH2"
        Me.BothCh_Display_RadioButton.UseVisualStyleBackColor = True
        '
        'DFT_CH1_Display_RadioButton
        '
        Me.DFT_CH1_Display_RadioButton.AutoSize = True
        Me.DFT_CH1_Display_RadioButton.Location = New System.Drawing.Point(240, 23)
        Me.DFT_CH1_Display_RadioButton.Name = "DFT_CH1_Display_RadioButton"
        Me.DFT_CH1_Display_RadioButton.Size = New System.Drawing.Size(70, 17)
        Me.DFT_CH1_Display_RadioButton.TabIndex = 5
        Me.DFT_CH1_Display_RadioButton.Text = "DFT CH1"
        Me.DFT_CH1_Display_RadioButton.UseVisualStyleBackColor = True
        '
        'XY_Display_RadioButton
        '
        Me.XY_Display_RadioButton.AutoSize = True
        Me.XY_Display_RadioButton.Location = New System.Drawing.Point(195, 23)
        Me.XY_Display_RadioButton.Name = "XY_Display_RadioButton"
        Me.XY_Display_RadioButton.Size = New System.Drawing.Size(39, 17)
        Me.XY_Display_RadioButton.TabIndex = 3
        Me.XY_Display_RadioButton.Text = "XY"
        Me.XY_Display_RadioButton.UseVisualStyleBackColor = True
        '
        'CH1_Display_RadioButton
        '
        Me.CH1_Display_RadioButton.AutoSize = True
        Me.CH1_Display_RadioButton.Location = New System.Drawing.Point(91, 23)
        Me.CH1_Display_RadioButton.Name = "CH1_Display_RadioButton"
        Me.CH1_Display_RadioButton.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CH1_Display_RadioButton.Size = New System.Drawing.Size(46, 17)
        Me.CH1_Display_RadioButton.TabIndex = 1
        Me.CH1_Display_RadioButton.Text = "CH1"
        Me.CH1_Display_RadioButton.UseVisualStyleBackColor = True
        '
        'CH2_Display_RadioButton
        '
        Me.CH2_Display_RadioButton.AutoSize = True
        Me.CH2_Display_RadioButton.Location = New System.Drawing.Point(143, 23)
        Me.CH2_Display_RadioButton.Name = "CH2_Display_RadioButton"
        Me.CH2_Display_RadioButton.Size = New System.Drawing.Size(46, 17)
        Me.CH2_Display_RadioButton.TabIndex = 2
        Me.CH2_Display_RadioButton.Text = "CH2"
        Me.CH2_Display_RadioButton.UseVisualStyleBackColor = True
        '
        'CH1_Gain_GroupBox
        '
        Me.CH1_Gain_GroupBox.Controls.Add(Me.CH1_Gain_RadioButton1)
        Me.CH1_Gain_GroupBox.Controls.Add(Me.CH1_Gain_RadioButton2)
        Me.CH1_Gain_GroupBox.Controls.Add(Me.CH1_Gain_RadioButton3)
        Me.CH1_Gain_GroupBox.Location = New System.Drawing.Point(929, 218)
        Me.CH1_Gain_GroupBox.Margin = New System.Windows.Forms.Padding(2)
        Me.CH1_Gain_GroupBox.Name = "CH1_Gain_GroupBox"
        Me.CH1_Gain_GroupBox.Size = New System.Drawing.Size(79, 87)
        Me.CH1_Gain_GroupBox.TabIndex = 5
        Me.CH1_Gain_GroupBox.TabStop = False
        Me.CH1_Gain_GroupBox.Text = "CH1 Gain"
        '
        'CH1_Gain_RadioButton1
        '
        Me.CH1_Gain_RadioButton1.AutoSize = True
        Me.CH1_Gain_RadioButton1.Checked = True
        Me.CH1_Gain_RadioButton1.Location = New System.Drawing.Point(8, 15)
        Me.CH1_Gain_RadioButton1.Margin = New System.Windows.Forms.Padding(2)
        Me.CH1_Gain_RadioButton1.Name = "CH1_Gain_RadioButton1"
        Me.CH1_Gain_RadioButton1.Size = New System.Drawing.Size(60, 17)
        Me.CH1_Gain_RadioButton1.TabIndex = 0
        Me.CH1_Gain_RadioButton1.TabStop = True
        Me.CH1_Gain_RadioButton1.Text = "GAIN 1"
        Me.CH1_Gain_RadioButton1.UseVisualStyleBackColor = True
        '
        'CH1_Gain_RadioButton2
        '
        Me.CH1_Gain_RadioButton2.AutoSize = True
        Me.CH1_Gain_RadioButton2.Location = New System.Drawing.Point(8, 38)
        Me.CH1_Gain_RadioButton2.Margin = New System.Windows.Forms.Padding(2)
        Me.CH1_Gain_RadioButton2.Name = "CH1_Gain_RadioButton2"
        Me.CH1_Gain_RadioButton2.Size = New System.Drawing.Size(60, 17)
        Me.CH1_Gain_RadioButton2.TabIndex = 1
        Me.CH1_Gain_RadioButton2.Text = "GAIN 2"
        Me.CH1_Gain_RadioButton2.UseVisualStyleBackColor = True
        '
        'CH1_Gain_RadioButton3
        '
        Me.CH1_Gain_RadioButton3.AutoSize = True
        Me.CH1_Gain_RadioButton3.Location = New System.Drawing.Point(8, 61)
        Me.CH1_Gain_RadioButton3.Margin = New System.Windows.Forms.Padding(2)
        Me.CH1_Gain_RadioButton3.Name = "CH1_Gain_RadioButton3"
        Me.CH1_Gain_RadioButton3.Size = New System.Drawing.Size(60, 17)
        Me.CH1_Gain_RadioButton3.TabIndex = 2
        Me.CH1_Gain_RadioButton3.Text = "GAIN 5"
        Me.CH1_Gain_RadioButton3.UseVisualStyleBackColor = True
        '
        'CH2_Gain_GroupBox
        '
        Me.CH2_Gain_GroupBox.Controls.Add(Me.CH2_Gain_RadioButton1)
        Me.CH2_Gain_GroupBox.Controls.Add(Me.CH2_Gain_RadioButton2)
        Me.CH2_Gain_GroupBox.Controls.Add(Me.CH2_Gain_RadioButton3)
        Me.CH2_Gain_GroupBox.Location = New System.Drawing.Point(929, 317)
        Me.CH2_Gain_GroupBox.Margin = New System.Windows.Forms.Padding(2)
        Me.CH2_Gain_GroupBox.Name = "CH2_Gain_GroupBox"
        Me.CH2_Gain_GroupBox.Size = New System.Drawing.Size(79, 87)
        Me.CH2_Gain_GroupBox.TabIndex = 6
        Me.CH2_Gain_GroupBox.TabStop = False
        Me.CH2_Gain_GroupBox.Text = "CH2 Gain"
        '
        'CH2_Gain_RadioButton1
        '
        Me.CH2_Gain_RadioButton1.AutoSize = True
        Me.CH2_Gain_RadioButton1.Checked = True
        Me.CH2_Gain_RadioButton1.Location = New System.Drawing.Point(8, 15)
        Me.CH2_Gain_RadioButton1.Margin = New System.Windows.Forms.Padding(2)
        Me.CH2_Gain_RadioButton1.Name = "CH2_Gain_RadioButton1"
        Me.CH2_Gain_RadioButton1.Size = New System.Drawing.Size(60, 17)
        Me.CH2_Gain_RadioButton1.TabIndex = 0
        Me.CH2_Gain_RadioButton1.TabStop = True
        Me.CH2_Gain_RadioButton1.Text = "GAIN 1"
        Me.CH2_Gain_RadioButton1.UseVisualStyleBackColor = True
        '
        'CH2_Gain_RadioButton2
        '
        Me.CH2_Gain_RadioButton2.AutoSize = True
        Me.CH2_Gain_RadioButton2.Location = New System.Drawing.Point(8, 38)
        Me.CH2_Gain_RadioButton2.Margin = New System.Windows.Forms.Padding(2)
        Me.CH2_Gain_RadioButton2.Name = "CH2_Gain_RadioButton2"
        Me.CH2_Gain_RadioButton2.Size = New System.Drawing.Size(60, 17)
        Me.CH2_Gain_RadioButton2.TabIndex = 1
        Me.CH2_Gain_RadioButton2.Text = "GAIN 2"
        Me.CH2_Gain_RadioButton2.UseVisualStyleBackColor = True
        '
        'CH2_Gain_RadioButton3
        '
        Me.CH2_Gain_RadioButton3.AutoSize = True
        Me.CH2_Gain_RadioButton3.Location = New System.Drawing.Point(8, 61)
        Me.CH2_Gain_RadioButton3.Margin = New System.Windows.Forms.Padding(2)
        Me.CH2_Gain_RadioButton3.Name = "CH2_Gain_RadioButton3"
        Me.CH2_Gain_RadioButton3.Size = New System.Drawing.Size(60, 17)
        Me.CH2_Gain_RadioButton3.TabIndex = 2
        Me.CH2_Gain_RadioButton3.Text = "GAIN 5"
        Me.CH2_Gain_RadioButton3.UseVisualStyleBackColor = True
        '
        'Run_Button
        '
        Me.Run_Button.Location = New System.Drawing.Point(748, 619)
        Me.Run_Button.Name = "Run_Button"
        Me.Run_Button.Size = New System.Drawing.Size(75, 37)
        Me.Run_Button.TabIndex = 8
        Me.Run_Button.Text = "RUN"
        Me.Run_Button.UseVisualStyleBackColor = True
        '
        'Export_CSV_Button
        '
        Me.Export_CSV_Button.Location = New System.Drawing.Point(933, 619)
        Me.Export_CSV_Button.Name = "Export_CSV_Button"
        Me.Export_CSV_Button.Size = New System.Drawing.Size(75, 36)
        Me.Export_CSV_Button.TabIndex = 12
        Me.Export_CSV_Button.Text = "SAVE"
        Me.Export_CSV_Button.UseVisualStyleBackColor = True
        '
        'CH2_Offset_VScrollBar
        '
        Me.CH2_Offset_VScrollBar.LargeChange = 2
        Me.CH2_Offset_VScrollBar.Location = New System.Drawing.Point(43, 35)
        Me.CH2_Offset_VScrollBar.Maximum = 0
        Me.CH2_Offset_VScrollBar.Minimum = -1023
        Me.CH2_Offset_VScrollBar.Name = "CH2_Offset_VScrollBar"
        Me.CH2_Offset_VScrollBar.Size = New System.Drawing.Size(16, 238)
        Me.CH2_Offset_VScrollBar.SmallChange = 2
        Me.CH2_Offset_VScrollBar.TabIndex = 13
        Me.CH2_Offset_VScrollBar.Value = -512
        '
        'CH1_Offset_VScrollBar
        '
        Me.CH1_Offset_VScrollBar.LargeChange = 2
        Me.CH1_Offset_VScrollBar.Location = New System.Drawing.Point(9, 35)
        Me.CH1_Offset_VScrollBar.Maximum = 0
        Me.CH1_Offset_VScrollBar.Minimum = -1023
        Me.CH1_Offset_VScrollBar.Name = "CH1_Offset_VScrollBar"
        Me.CH1_Offset_VScrollBar.Size = New System.Drawing.Size(16, 238)
        Me.CH1_Offset_VScrollBar.SmallChange = 2
        Me.CH1_Offset_VScrollBar.TabIndex = 16
        Me.CH1_Offset_VScrollBar.Value = -512
        '
        'C
        '
        Me.C.Controls.Add(Me.CH2_Offset_TextBox)
        Me.C.Controls.Add(Me.CH1_Offset_TextBox)
        Me.C.Controls.Add(Me.CH2_offset_Value_Label)
        Me.C.Controls.Add(Me.CH1_offset_Value_Label)
        Me.C.Controls.Add(Me.CH2_Label)
        Me.C.Controls.Add(Me.CH2_Offset_VScrollBar)
        Me.C.Controls.Add(Me.CH1_Offset_VScrollBar)
        Me.C.Controls.Add(Me.CH1_Label)
        Me.C.Location = New System.Drawing.Point(748, 224)
        Me.C.Name = "C"
        Me.C.Size = New System.Drawing.Size(72, 375)
        Me.C.TabIndex = 17
        Me.C.TabStop = False
        Me.C.Text = "OFFSET"
        '
        'CH2_Offset_TextBox
        '
        Me.CH2_Offset_TextBox.Location = New System.Drawing.Point(4, 340)
        Me.CH2_Offset_TextBox.Name = "CH2_Offset_TextBox"
        Me.CH2_Offset_TextBox.Size = New System.Drawing.Size(68, 20)
        Me.CH2_Offset_TextBox.TabIndex = 20
        Me.CH2_Offset_TextBox.Text = "CH2 Offset"
        '
        'CH1_Offset_TextBox
        '
        Me.CH1_Offset_TextBox.Location = New System.Drawing.Point(4, 297)
        Me.CH1_Offset_TextBox.Name = "CH1_Offset_TextBox"
        Me.CH1_Offset_TextBox.Size = New System.Drawing.Size(68, 20)
        Me.CH1_Offset_TextBox.TabIndex = 19
        Me.CH1_Offset_TextBox.Text = "CH1_Offset"
        '
        'CH2_offset_Value_Label
        '
        Me.CH2_offset_Value_Label.AutoSize = True
        Me.CH2_offset_Value_Label.Location = New System.Drawing.Point(6, 321)
        Me.CH2_offset_Value_Label.Name = "CH2_offset_Value_Label"
        Me.CH2_offset_Value_Label.Size = New System.Drawing.Size(28, 13)
        Me.CH2_offset_Value_Label.TabIndex = 18
        Me.CH2_offset_Value_Label.Text = "CH2"
        '
        'CH1_offset_Value_Label
        '
        Me.CH1_offset_Value_Label.AutoSize = True
        Me.CH1_offset_Value_Label.Location = New System.Drawing.Point(5, 283)
        Me.CH1_offset_Value_Label.Name = "CH1_offset_Value_Label"
        Me.CH1_offset_Value_Label.Size = New System.Drawing.Size(28, 13)
        Me.CH1_offset_Value_Label.TabIndex = 17
        Me.CH1_offset_Value_Label.Text = "CH1"
        '
        'CH2_Label
        '
        Me.CH2_Label.AutoSize = True
        Me.CH2_Label.Location = New System.Drawing.Point(40, 20)
        Me.CH2_Label.Name = "CH2_Label"
        Me.CH2_Label.Size = New System.Drawing.Size(28, 13)
        Me.CH2_Label.TabIndex = 1
        Me.CH2_Label.Text = "CH2"
        '
        'CH1_Label
        '
        Me.CH1_Label.AutoSize = True
        Me.CH1_Label.Location = New System.Drawing.Point(6, 20)
        Me.CH1_Label.Name = "CH1_Label"
        Me.CH1_Label.Size = New System.Drawing.Size(28, 13)
        Me.CH1_Label.TabIndex = 0
        Me.CH1_Label.Text = "CH1"
        '
        'Trig_Level_GroupBox
        '
        Me.Trig_Level_GroupBox.Controls.Add(Me.CH1_Trig_TextBox)
        Me.Trig_Level_GroupBox.Controls.Add(Me.CH1_Trig_Level_Label)
        Me.Trig_Level_GroupBox.Controls.Add(Me.CH1_TrigLevel_VScrollBar)
        Me.Trig_Level_GroupBox.Controls.Add(Me.Label2)
        Me.Trig_Level_GroupBox.Location = New System.Drawing.Point(836, 221)
        Me.Trig_Level_GroupBox.Name = "Trig_Level_GroupBox"
        Me.Trig_Level_GroupBox.Size = New System.Drawing.Size(83, 375)
        Me.Trig_Level_GroupBox.TabIndex = 18
        Me.Trig_Level_GroupBox.TabStop = False
        Me.Trig_Level_GroupBox.Text = "TRIG LEVEL"
        '
        'CH1_Trig_TextBox
        '
        Me.CH1_Trig_TextBox.Location = New System.Drawing.Point(13, 300)
        Me.CH1_Trig_TextBox.Name = "CH1_Trig_TextBox"
        Me.CH1_Trig_TextBox.Size = New System.Drawing.Size(59, 20)
        Me.CH1_Trig_TextBox.TabIndex = 20
        Me.CH1_Trig_TextBox.Text = "Trig_Level"
        '
        'CH1_Trig_Level_Label
        '
        Me.CH1_Trig_Level_Label.AutoSize = True
        Me.CH1_Trig_Level_Label.Location = New System.Drawing.Point(10, 283)
        Me.CH1_Trig_Level_Label.Name = "CH1_Trig_Level_Label"
        Me.CH1_Trig_Level_Label.Size = New System.Drawing.Size(54, 13)
        Me.CH1_Trig_Level_Label.TabIndex = 17
        Me.CH1_Trig_Level_Label.Text = "CH1/CH2"
        '
        'CH1_TrigLevel_VScrollBar
        '
        Me.CH1_TrigLevel_VScrollBar.LargeChange = 2
        Me.CH1_TrigLevel_VScrollBar.Location = New System.Drawing.Point(31, 35)
        Me.CH1_TrigLevel_VScrollBar.Maximum = 0
        Me.CH1_TrigLevel_VScrollBar.Minimum = -1023
        Me.CH1_TrigLevel_VScrollBar.Name = "CH1_TrigLevel_VScrollBar"
        Me.CH1_TrigLevel_VScrollBar.Size = New System.Drawing.Size(16, 238)
        Me.CH1_TrigLevel_VScrollBar.SmallChange = 2
        Me.CH1_TrigLevel_VScrollBar.TabIndex = 16
        Me.CH1_TrigLevel_VScrollBar.Value = -512
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(19, 22)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(54, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "CH1/CH2"
        '
        'Capture_Mode_GroupBox
        '
        Me.Capture_Mode_GroupBox.Controls.Add(Me.Single_RadioButton)
        Me.Capture_Mode_GroupBox.Controls.Add(Me.Continuous_RadioButton)
        Me.Capture_Mode_GroupBox.Controls.Add(Me.OverWrite_RadioButton)
        Me.Capture_Mode_GroupBox.Location = New System.Drawing.Point(929, 504)
        Me.Capture_Mode_GroupBox.Margin = New System.Windows.Forms.Padding(2)
        Me.Capture_Mode_GroupBox.Name = "Capture_Mode_GroupBox"
        Me.Capture_Mode_GroupBox.Size = New System.Drawing.Size(79, 92)
        Me.Capture_Mode_GroupBox.TabIndex = 19
        Me.Capture_Mode_GroupBox.TabStop = False
        Me.Capture_Mode_GroupBox.Text = "Capture Mode"
        '
        'Single_RadioButton
        '
        Me.Single_RadioButton.AutoSize = True
        Me.Single_RadioButton.Checked = True
        Me.Single_RadioButton.Location = New System.Drawing.Point(8, 15)
        Me.Single_RadioButton.Margin = New System.Windows.Forms.Padding(2)
        Me.Single_RadioButton.Name = "Single_RadioButton"
        Me.Single_RadioButton.Size = New System.Drawing.Size(64, 17)
        Me.Single_RadioButton.TabIndex = 0
        Me.Single_RadioButton.TabStop = True
        Me.Single_RadioButton.Text = "SINGLE"
        Me.Single_RadioButton.UseVisualStyleBackColor = True
        '
        'Continuous_RadioButton
        '
        Me.Continuous_RadioButton.AutoSize = True
        Me.Continuous_RadioButton.Location = New System.Drawing.Point(8, 36)
        Me.Continuous_RadioButton.Margin = New System.Windows.Forms.Padding(2)
        Me.Continuous_RadioButton.Name = "Continuous_RadioButton"
        Me.Continuous_RadioButton.Size = New System.Drawing.Size(68, 17)
        Me.Continuous_RadioButton.TabIndex = 1
        Me.Continuous_RadioButton.Text = "REPEAT"
        Me.Continuous_RadioButton.UseVisualStyleBackColor = True
        '
        'OverWrite_RadioButton
        '
        Me.OverWrite_RadioButton.AutoSize = True
        Me.OverWrite_RadioButton.Location = New System.Drawing.Point(8, 57)
        Me.OverWrite_RadioButton.Margin = New System.Windows.Forms.Padding(2)
        Me.OverWrite_RadioButton.Name = "OverWrite_RadioButton"
        Me.OverWrite_RadioButton.Size = New System.Drawing.Size(62, 17)
        Me.OverWrite_RadioButton.TabIndex = 2
        Me.OverWrite_RadioButton.Text = "STORE"
        Me.OverWrite_RadioButton.UseVisualStyleBackColor = True
        '
        'Number_of_Samples_GroupBox
        '
        Me.Number_of_Samples_GroupBox.Controls.Add(Me.Samples_RadioButton1)
        Me.Number_of_Samples_GroupBox.Controls.Add(Me.Samples_RadioButton2)
        Me.Number_of_Samples_GroupBox.Controls.Add(Me.Samples_RadioButton3)
        Me.Number_of_Samples_GroupBox.Location = New System.Drawing.Point(929, 413)
        Me.Number_of_Samples_GroupBox.Margin = New System.Windows.Forms.Padding(2)
        Me.Number_of_Samples_GroupBox.Name = "Number_of_Samples_GroupBox"
        Me.Number_of_Samples_GroupBox.Size = New System.Drawing.Size(79, 87)
        Me.Number_of_Samples_GroupBox.TabIndex = 6
        Me.Number_of_Samples_GroupBox.TabStop = False
        Me.Number_of_Samples_GroupBox.Text = "Mode"
        '
        'Samples_RadioButton1
        '
        Me.Samples_RadioButton1.AutoSize = True
        Me.Samples_RadioButton1.Checked = True
        Me.Samples_RadioButton1.Location = New System.Drawing.Point(5, 18)
        Me.Samples_RadioButton1.Margin = New System.Windows.Forms.Padding(2)
        Me.Samples_RadioButton1.Name = "Samples_RadioButton1"
        Me.Samples_RadioButton1.Size = New System.Drawing.Size(58, 17)
        Me.Samples_RadioButton1.TabIndex = 3
        Me.Samples_RadioButton1.TabStop = True
        Me.Samples_RadioButton1.Text = "Normal"
        Me.Samples_RadioButton1.UseVisualStyleBackColor = True
        '
        'Samples_RadioButton2
        '
        Me.Samples_RadioButton2.AutoSize = True
        Me.Samples_RadioButton2.Location = New System.Drawing.Point(5, 39)
        Me.Samples_RadioButton2.Margin = New System.Windows.Forms.Padding(2)
        Me.Samples_RadioButton2.Name = "Samples_RadioButton2"
        Me.Samples_RadioButton2.Size = New System.Drawing.Size(68, 17)
        Me.Samples_RadioButton2.TabIndex = 1
        Me.Samples_RadioButton2.Text = "FFT CH1"
        Me.Samples_RadioButton2.UseVisualStyleBackColor = True
        '
        'Samples_RadioButton3
        '
        Me.Samples_RadioButton3.AutoSize = True
        Me.Samples_RadioButton3.Location = New System.Drawing.Point(5, 60)
        Me.Samples_RadioButton3.Margin = New System.Windows.Forms.Padding(2)
        Me.Samples_RadioButton3.Name = "Samples_RadioButton3"
        Me.Samples_RadioButton3.Size = New System.Drawing.Size(68, 17)
        Me.Samples_RadioButton3.TabIndex = 2
        Me.Samples_RadioButton3.Text = "FFT CH2"
        Me.Samples_RadioButton3.UseVisualStyleBackColor = True
        '
        'Set_Sample_RateGroupBox
        '
        Me.Set_Sample_RateGroupBox.Controls.Add(Me.Sample_Rate_ListBox)
        Me.Set_Sample_RateGroupBox.Location = New System.Drawing.Point(748, 125)
        Me.Set_Sample_RateGroupBox.Name = "Set_Sample_RateGroupBox"
        Me.Set_Sample_RateGroupBox.Size = New System.Drawing.Size(262, 90)
        Me.Set_Sample_RateGroupBox.TabIndex = 0
        Me.Set_Sample_RateGroupBox.TabStop = False
        Me.Set_Sample_RateGroupBox.Text = "Set Sample Rate"
        '
        'Sample_Rate_ListBox
        '
        Me.Sample_Rate_ListBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Sample_Rate_ListBox.FormattingEnabled = True
        Me.Sample_Rate_ListBox.ItemHeight = 16
        Me.Sample_Rate_ListBox.Items.AddRange(New Object() {"20Mbps 0.05us/sample (ETS)", "10Mbps 0.10us/sample (ETS)", "5Mbps  0.20us/sample (ETS)", "2Mbps  0.50us/sample (ETS)", "1Mbps   1.0us/sample (ETS)", "500kbps 2.0us/sample (ETS)", "200kbps 5.0us/sample (ETS)", "100kbps  10us/sample", "50kbps   20us/sample", "20kbps   50us/sample", "10kbps  100us/sample", "5kbps   200us/sample", "2kbps   500us/sample", "1kbps   1.0ms/sample", "500Hz   2.0ms/sample", "200Hz   5.0ms/sample", "100Hz    10ms/sample ", "50Hz     20ms/sample", "20Hz     50ms/sample", "10Hz    100ms/sample "})
        Me.Sample_Rate_ListBox.Location = New System.Drawing.Point(25, 19)
        Me.Sample_Rate_ListBox.Name = "Sample_Rate_ListBox"
        Me.Sample_Rate_ListBox.Size = New System.Drawing.Size(207, 52)
        Me.Sample_Rate_ListBox.TabIndex = 27
        '
        'Stop_Button
        '
        Me.Stop_Button.Location = New System.Drawing.Point(839, 619)
        Me.Stop_Button.Name = "Stop_Button"
        Me.Stop_Button.Size = New System.Drawing.Size(75, 38)
        Me.Stop_Button.TabIndex = 20
        Me.Stop_Button.Text = "STOP"
        Me.Stop_Button.UseVisualStyleBackColor = True
        '
        'Graph_Title_TextBox
        '
        Me.Graph_Title_TextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Graph_Title_TextBox.Location = New System.Drawing.Point(252, 662)
        Me.Graph_Title_TextBox.Name = "Graph_Title_TextBox"
        Me.Graph_Title_TextBox.Size = New System.Drawing.Size(310, 22)
        Me.Graph_Title_TextBox.TabIndex = 24
        Me.Graph_Title_TextBox.Text = "Enter Graph Title"
        Me.Graph_Title_TextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'EXIT_Button
        '
        Me.EXIT_Button.Location = New System.Drawing.Point(839, 663)
        Me.EXIT_Button.Name = "EXIT_Button"
        Me.EXIT_Button.Size = New System.Drawing.Size(75, 23)
        Me.EXIT_Button.TabIndex = 25
        Me.EXIT_Button.Text = "EXIT"
        Me.EXIT_Button.UseVisualStyleBackColor = True
        '
        'Aj_ZG1
        '
        Me.Aj_ZG1.Location = New System.Drawing.Point(15, 81)
        Me.Aj_ZG1.Name = "Aj_ZG1"
        Me.Aj_ZG1.ScrollGrace = 0
        Me.Aj_ZG1.ScrollMaxX = 0
        Me.Aj_ZG1.ScrollMaxY = 0
        Me.Aj_ZG1.ScrollMaxY2 = 0
        Me.Aj_ZG1.ScrollMinX = 0
        Me.Aj_ZG1.ScrollMinY = 0
        Me.Aj_ZG1.ScrollMinY2 = 0
        Me.Aj_ZG1.Size = New System.Drawing.Size(15, 17)
        Me.Aj_ZG1.TabIndex = 23
        '
        'Opening_Panel
        '
        Me.Opening_Panel.Controls.Add(Me.RichTextBox)
        Me.Opening_Panel.Location = New System.Drawing.Point(12, 81)
        Me.Opening_Panel.Name = "Opening_Panel"
        Me.Opening_Panel.Size = New System.Drawing.Size(721, 518)
        Me.Opening_Panel.TabIndex = 28
        '
        'RichTextBox
        '
        Me.RichTextBox.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.RichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.RichTextBox.Font = New System.Drawing.Font("Comic Sans MS", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RichTextBox.Location = New System.Drawing.Point(36, 5)
        Me.RichTextBox.Name = "RichTextBox"
        Me.RichTextBox.ReadOnly = True
        Me.RichTextBox.Size = New System.Drawing.Size(653, 492)
        Me.RichTextBox.TabIndex = 0
        Me.RichTextBox.Text = resources.GetString("RichTextBox.Text")
        '
        'TestLEDButton
        '
        Me.TestLEDButton.Location = New System.Drawing.Point(549, 619)
        Me.TestLEDButton.Name = "TestLEDButton"
        Me.TestLEDButton.Size = New System.Drawing.Size(89, 27)
        Me.TestLEDButton.TabIndex = 31
        Me.TestLEDButton.Text = "Test LED"
        Me.TestLEDButton.UseVisualStyleBackColor = True
        '
        'SETUP_Button
        '
        Me.SETUP_Button.Location = New System.Drawing.Point(644, 619)
        Me.SETUP_Button.Name = "SETUP_Button"
        Me.SETUP_Button.Size = New System.Drawing.Size(89, 27)
        Me.SETUP_Button.TabIndex = 36
        Me.SETUP_Button.Text = "SETUP"
        Me.SETUP_Button.UseVisualStyleBackColor = True
        '
        'Scope_Main_Form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1028, 730)
        Me.Controls.Add(Me.SETUP_Button)
        Me.Controls.Add(Me.TestLEDButton)
        Me.Controls.Add(Me.Aj_ZG1)
        Me.Controls.Add(Me.EXIT_Button)
        Me.Controls.Add(Me.Graph_Title_TextBox)
        Me.Controls.Add(Me.Stop_Button)
        Me.Controls.Add(Me.Set_Sample_RateGroupBox)
        Me.Controls.Add(Me.Connect_GroupBox)
        Me.Controls.Add(Me.Capture_Mode_GroupBox)
        Me.Controls.Add(Me.C)
        Me.Controls.Add(Me.Number_of_Samples_GroupBox)
        Me.Controls.Add(Me.Trig_Level_GroupBox)
        Me.Controls.Add(Me.Export_CSV_Button)
        Me.Controls.Add(Me.Run_Button)
        Me.Controls.Add(Me.Display_GroupBox)
        Me.Controls.Add(Me.CH2_Gain_GroupBox)
        Me.Controls.Add(Me.CH1_Gain_GroupBox)
        Me.Controls.Add(Me.Mode_GroupBox)
        Me.Controls.Add(Me.Opening_Panel)
        Me.Name = "Scope_Main_Form"
        Me.Text = "Aj-Simple-Scope"
        Me.Connect_GroupBox.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.Mode_GroupBox.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Display_GroupBox.ResumeLayout(False)
        Me.Display_GroupBox.PerformLayout()
        Me.CH1_Gain_GroupBox.ResumeLayout(False)
        Me.CH1_Gain_GroupBox.PerformLayout()
        Me.CH2_Gain_GroupBox.ResumeLayout(False)
        Me.CH2_Gain_GroupBox.PerformLayout()
        Me.C.ResumeLayout(False)
        Me.C.PerformLayout()
        Me.Trig_Level_GroupBox.ResumeLayout(False)
        Me.Trig_Level_GroupBox.PerformLayout()
        Me.Capture_Mode_GroupBox.ResumeLayout(False)
        Me.Capture_Mode_GroupBox.PerformLayout()
        Me.Number_of_Samples_GroupBox.ResumeLayout(False)
        Me.Number_of_Samples_GroupBox.PerformLayout()
        Me.Set_Sample_RateGroupBox.ResumeLayout(False)
        Me.Opening_Panel.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Mode_GroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents CH2_Trig_RadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents Ch1_Trig_RadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents Auto_Trig_RadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents HL_Trig_RadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents LH_Trig_RadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents Display_GroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents DFT_CH1_Display_RadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents XY_Display_RadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents CH2_Display_RadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents CH1_Display_RadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents BothCh_Display_RadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents DFT_CH2_Display_RadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents CH1_Gain_GroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents CH1_Gain_RadioButton3 As System.Windows.Forms.RadioButton
    Friend WithEvents CH1_Gain_RadioButton2 As System.Windows.Forms.RadioButton
    Friend WithEvents CH1_Gain_RadioButton1 As System.Windows.Forms.RadioButton
    Friend WithEvents CH2_Gain_GroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents CH2_Gain_RadioButton1 As System.Windows.Forms.RadioButton
    Friend WithEvents CH2_Gain_RadioButton2 As System.Windows.Forms.RadioButton
    Friend WithEvents CH2_Gain_RadioButton3 As System.Windows.Forms.RadioButton
    Friend WithEvents Run_Button As System.Windows.Forms.Button
    Friend WithEvents Export_CSV_Button As System.Windows.Forms.Button
    Friend WithEvents CH2_Offset_VScrollBar As System.Windows.Forms.VScrollBar
    Friend WithEvents CH1_Offset_VScrollBar As System.Windows.Forms.VScrollBar
    Friend WithEvents C As System.Windows.Forms.GroupBox
    Friend WithEvents CH2_Label As System.Windows.Forms.Label
    Friend WithEvents CH1_Label As System.Windows.Forms.Label
    Friend WithEvents Trig_Level_GroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents CH1_TrigLevel_VScrollBar As System.Windows.Forms.VScrollBar
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Capture_Mode_GroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents Single_RadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents Continuous_RadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents OverWrite_RadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents Number_of_Samples_GroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents Samples_RadioButton2 As System.Windows.Forms.RadioButton
    Friend WithEvents Samples_RadioButton3 As System.Windows.Forms.RadioButton
    Friend WithEvents CH2_offset_Value_Label As System.Windows.Forms.Label
    Friend WithEvents CH1_offset_Value_Label As System.Windows.Forms.Label
    Friend WithEvents CH1_Trig_Level_Label As System.Windows.Forms.Label
    Friend WithEvents CH2_Offset_TextBox As System.Windows.Forms.TextBox
    Friend WithEvents CH1_Offset_TextBox As System.Windows.Forms.TextBox
    Friend WithEvents CH1_Trig_TextBox As System.Windows.Forms.TextBox
    Friend WithEvents Connect_GroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents Test_TextBox As System.Windows.Forms.TextBox
    Friend WithEvents Com_Ports As System.Windows.Forms.ListBox
    Friend WithEvents Get_Ports_Button As System.Windows.Forms.Button
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Set_Sample_RateGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents Stop_Button As System.Windows.Forms.Button
    Friend WithEvents Graph_Title_TextBox As System.Windows.Forms.TextBox
    Friend WithEvents Sample_Rate_ListBox As System.Windows.Forms.ListBox
    Friend WithEvents EXIT_Button As System.Windows.Forms.Button
    Friend WithEvents Aj_ZG1 As ZedGraph.ZedGraphControl
    Friend WithEvents Opening_Panel As System.Windows.Forms.Panel
    Friend WithEvents RichTextBox As System.Windows.Forms.RichTextBox
    Friend WithEvents TestLEDButton As System.Windows.Forms.Button
    Friend WithEvents Samples_RadioButton1 As System.Windows.Forms.RadioButton
    Friend WithEvents SETUP_Button As System.Windows.Forms.Button

End Class

#Region "Plot"
    Private Sub Plot_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Plot_Button.Click
        Dim rowno As Integer

        'Check what_to_plot
        what_to_plot()
        'Dim display_y1, display_y2, display_xy, display_dft1, display_dft2 As Boolean
        'Dim DataArray(401, 4) As String
        'Dim xcolumn, ycolumn, y1column As Integer
        Plot_Error = False 'we will check for DBNull's and exit
        'Start of Plot
        Dim myPane As GraphPane = Aj_ZG1.GraphPane
        'Clear Previous Graph if Over Plot is Not Checked
        If Overplot = True Then
        Else
            Aj_ZG1.GraphPane.CurveList.Clear()
            Aj_ZG1.GraphPane.GraphObjList.Clear()
        End If

        ' Set the Title
        myPane.Title.Text = "Aj Test Plot" 'Title_TextBox.Text

        Dim list As New PointPairList
        Dim list2 As New PointPairList
        Dim x, y, y1 As String 'Double
        
        'Proceed with data processing Plotting only if no null values are detected
        'Populate the List assume no null values
        If Plot_Error = False Then
            For rowno = 1 To (Data_Length + 1)
                'x and y data
                x = DataArray(rowno, xcolumn)
                y = DataArray(rowno, ycolumn)
                list.Add(x, y)

                'Data for y1
                If display_y1 = True Then
                    y1 = DataArray(rowno, y1column)
                    list2.Add(x, y1)
                End If
            Next



            ' Generate a red curve with diamond symbols, and "Alpha" in the legend
            Dim myCurve As LineItem
            If Symbol_RadioButton.Checked Then
                myCurve = myPane.AddCurve("Dog", list, Color.Red, SymbolType.Diamond)
                ' Fill the symbols with white
                myCurve.Symbol.Fill = New Fill(Color.White)
            Else
                myCurve = myPane.AddCurve("Dog", list, Color.Red, SymbolType.None) 'No Symbols
            End If



            ' Generate a blue curve with circle symbols, and "Beta" in the legend
            If display_y1 = True Then
                If Symbol_RadioButton.Checked Then
                    myCurve = myPane.AddCurve("Rat", list2, Color.Blue, SymbolType.Circle)
                    ' Fill the symbols with white
                    myCurve.Symbol.Fill = New Fill(Color.White)
                    ' Associate this curve with the Y2 axis
                    myCurve.IsY2Axis = True
                Else
                    myCurve = myPane.AddCurve("Elephant", list2, Color.Blue, SymbolType.None)
                    ' Fill the symbols with white
                    myCurve.Symbol.Fill = New Fill(Color.White)
                    ' Associate this curve with the Y2 axis
                    myCurve.IsY2Axis = True
                End If

            End If

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

            myPane.YAxis.Scale.Min = -10
            myPane.YAxis.Scale.Max = 10
            myPane.XAxis.Scale.Min = 0
            myPane.XAxis.Scale.Max = Data_Length


            'Enable the Y and Y2 axis display
            myPane.YAxis.IsVisible = True

            If display_y1 = True Then
                ' Enable the Y2 axis display
                myPane.Y2Axis.IsVisible = True
            End If

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
            '
            Dim mytext As New TextObj("Aj-" _
            & "S" & "c" & "o" & "p" & "e", 0.02F, 0.97F, _
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

        Else
            MessageBox.Show("Null Entries Set To 0 - Exiting")
        End If
    End Sub
    Private Sub Aj_ZG1_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs)
        SetSize()
    End Sub
    Private Sub SetSize()
        'Dim loc As New Point(0, 140)
        'Aj_ZG1.Location = loc()
        ' Leave a small margin around the outside of the control
        Dim size1 As New Size(Me.ClientRectangle.Width - 200, Me.ClientRectangle.Height - 200) 'Rectangle
        Dim size2 As New Size(Me.ClientRectangle.Height - 200, Me.ClientRectangle.Height - 200) 'Square

        'Dim size As New Size(Me.ClientRectangle.Height - 50, Me.ClientRectangle.Height - 50)
        If display_y1 = True Then
            Aj_ZG1.Size = size1
        Else
            Aj_ZG1.Size = size2
        End If

    End Sub
    Private Sub what_to_plot()
        'Dim display_x, display_y, display_y1 As Boolean
        'Dim xcolumn, ycolumn, y1column As Integer

        display_y = False
        display_y1 = False

        ' XLabel_TextBox.Text

        If BothCh_Display_RadioButton.Checked Then
            display_y = True
            display_y1 = True
            xcolumn = 0
            ycolumn = 1
            y1column = 2
            myPane.YAxis.Title.Text = "Ch1 Volts"
            myPane.Y2Axis.Title.Text = "Ch2 Volts"
            myPane.XAxis.Title.Text = Sample_Rate_DomainUpDown.Text
        ElseIf CH1_Display_RadioButton.Checked() Then
            display_y = True
            xcolumn = 0
            ycolumn = 1
            myPane.YAxis.Title.Text = "Ch1 Volts"
            myPane.XAxis.Title.Text = Sample_Rate_DomainUpDown.Text
        ElseIf CH2_Display_RadioButton.Checked() Then
            display_y = True
            xcolumn = 0
            ycolumn = 2
            myPane.YAxis.Title.Text = "Ch2 Volts"
            myPane.XAxis.Title.Text = Sample_Rate_DomainUpDown.Text
        ElseIf XY_Display_RadioButton.Checked() Then
            display_y = True
            xcolumn = 1
            ycolumn = 2
            myPane.XAxis.Title.Text = "Ch1 Volts"
            myPane.YAxis.Title.Text = "Ch2 Volts"
        ElseIf DFT_CH1_Display_RadioButton.Checked() Then
            display_y = True
            xcolumn = 5
            ycolumn = 3
            myPane.YAxis.Title.Text = "Ch1 Volts"
            myPane.XAxis.Title.Text = "Frequency"
        ElseIf DFT_CH2_Display_RadioButton.Checked() Then
            display_y = True
            xcolumn = 5
            ycolumn = 4
            myPane.YAxis.Title.Text = "Ch1 Volts"
            myPane.XAxis.Title.Text = "Frequency"
        End If
    End Sub
#End Region
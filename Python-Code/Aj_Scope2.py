#Scope2 Python Development Started 26 Aug 2013
#Based on AjScope2_06.py and Aj_PlotGui2Axis_3b_Good.py
#Started intefration of the GUI 16 Sept 13

#version 01 : Basic GUI interated and first level of plotting with capture
#19 Sept added some version of stop and abort

#version 02 : removed extra imports to see if it becomes faster
#Removed numpy,binascii
#Reduced most sleep times to 0.01sec

#plot broken up for ax1.ax2,ax3 & write labels
#mode setting for axis
#changed to row wise data array

#version 03: Continuing with incorporation of different modes
#corrected for 12.5V
#corrected the offset and trigger schemes

#version 4: Changing data to 401 length
#capture_data modified for different modes
#Store and Load modified
#Plot title entry widget added
#fft modes added

#version5: Single-repeat-store added
#ch1/ch2 mode reduced to 200data only fft=400 data
#load store abort deleted and save made next to run
#extra print commented
#diagnostic labels removed


#--------------------imports------------------------
import os
import sys
from Tkinter import *
import Tkinter, tkFileDialog
import ttk
import tkMessageBox
import serial
import glob
import math
from time import sleep
import csv
import numpy as np
import matplotlib.pyplot as plt
from matplotlib.backends.backend_tkagg import FigureCanvasTkAgg, \
NavigationToolbar2TkAgg

#-------------------Global Variables------------------------------
#-----------"Gain, Offset and Trigger Level, Vref"----------------
Ch1_Gain,Ch2_Gain=int(1),int(1)
Scale_Factor=float(1.0)
#-----------Cursor--------------------
xx,yy=float(),float()
cursorReqd=True
cursorColourFlag=True
cursorWidthFlag=True

#--------data-----------
data = [[0 for i in range(401)] for j in range(6)] #6 colmns 201rows
data[0][0]='time' #row column
data[1][0]='ch1Data'
data[2][0]='ch2data'
data[3][0]='frequency'
data[4][0]='ch1dftdata'
data[5][0]='ch2dftdata'

x = [0 for i in range(400)]
y = [0 for i in range(400)]
y1 = [0 for i in range(400)]

#----------plot------------------
v1=(0,199,-12.5,12.5)#CH1 gain 1
v2=(0,199,-12.5,12.5)#CH2 gain 1
v3=(0,199,-12.5,12.5)#cursor axis gain 1
ch=str('2')
global ax1,ax2,ax3
ax1on,ax2on,ax3on=True,True,True
overWrite=False
YAxis_Text,Y2Axis_Text,XAxis_Text='','',''
maxCh1=12.
maxCh2=12.
#----------run-----------
keepRunning=True
runStatus=False #not Busy
#---------Select Mode----------
Data_Length = 200
DFT = False
Read_Mode = 1 #200 CH1 "200 CH2
#----------Select Sampling
ETS_Error = 1 #Run only if not in ETS with Auto
ETS = 1
Heading1 = "Time(uSec)" #The Plot X-Axis label
Multiplier = 2.0   # For calculating the sampling rate
Max_Frequency = 250000 # The DFT Max frequency
#--------Start Main Program------------------------------------------
#--------Main Frame Container
mainFrame = Tkinter.Tk()
mainFrame.wm_title("Aj-Scope")
mainFrame.wm_geometry('1024x768+0+0') #size, offset
mainFrame.configure(background="#ece9d8")

# --------------GUI-----------------
##root = Tk()
##root.wm_geometry('800x600+100+100')

# create the general figure
fig = plt.figure()

canvas = FigureCanvasTkAgg(fig, master=mainFrame)
canvas.get_tk_widget().place(width=640, height=480,x=50,y=100)

#outside background colour
rect = fig.figurePatch # a rectangle instance
rect.set_facecolor('lightgoldenrodyellow')

#-------Customising the root ttk style
myStyle = ttk.Style()
myStyle.configure('.', font=('Ariel', '9'))

#------ Navigation Toolbar
mytoolbar=NavigationToolbar2TkAgg(canvas,mainFrame)
mytoolbar.place(width=210, height=30,x=50,y=603)

#---------------Cursor and Plot----------------------------------------
def myCursor():
    x1,y1,x2,y2=int(0),int(0),int(0),int(0)

    def mouse_move(event):
        global xx,yy
        #plt.clf()

        if not event.inaxes: return

        xx, yy = event.xdata, event.ydata
        #print xx,yy

        # update the line positions
        lx.set_ydata(yy )
        ly.set_xdata(xx)

        #Set the text



        #Set Cursor Colour
        if  (cursorColourFlag==True):
            lx.set_color('r')
            ly.set_color('r')
            txt.set_color('r')
            yy=yy*(v1[3]/v3[3])
        else:
            lx.set_color('b')
            ly.set_color('b')
            txt.set_color('b')
            yy=yy*(v2[3]/v3[3])


        #Set Cursor Visibility by Width & Test Visibility by background color
        if  (cursorWidthFlag==True):
            lx.set_linewidth(1)
            ly.set_linewidth(1)
        else:
            lx.set_linewidth(0)
            ly.set_linewidth(0)
            txt.set_color('lightgoldenrodyellow')

        #Set the text
        txt.set_text( 'x=%1.3f, y=%1.3f'%(xx,yy) )


        canvas.draw()

    def onclick(event):
        """Deal with click events"""
        global ch,xypos1,xypos2,yy,xx,cursorColourFlag,cursorWidthFlag

        button = ['left','middle','right']# '1','2','3'
        #print event.button
        ch=str(event.button)
        #print ch

        #Toggle cursorColourFlag on Left Mouse
        #Toggle cursorWidthFlag  on Right Mouse


        if  (ch=='1'):
            cursorColourFlag= not cursorColourFlag
        elif(ch=='3'):
            cursorWidthFlag=not cursorWidthFlag

        #if event.dblclick:
        #   print 'double click on ', event.button

    #Instansation

    ax = ax3

    lx = ax.axhline(linewidth=0)#linestyle='-.',linewidth=1)#color='g')  # the horiz line
    ly = ax.axvline(linewidth=0)#linestyle='-.',linewidth=1)#color='g')  # the vert line


    # position for text in relative axes coords
    txt = ax.text( 0.7, 1.05, '', transform=ax.transAxes)
##    txt = ax.text( 0.7, 1.05, '', transform=ax.transAxes,fontsize=12, \
##    bbox=dict(facecolor='red', alpha=0.2),color='blue')

    fig.canvas.mpl_connect('motion_notify_event', mouse_move)
    fig.canvas.mpl_connect('button_press_event',onclick)



def plot():
    global ax1, ax2,ax3,cursorReqd,x,y,y1,v1,v2,v3
    global YAxis_Text,Y2Axis_Text,XAxis_Text
    global Data_Length, Multiplier,maxCh1,maxCh2

    #print 'cursorReqd=', cursorReqd
    if(cursorReqd==False)and (overWrite==False):
        ax1.cla()
        ax2.cla()
##    elif(cursorReqd==True)and (overWrite=True):
##        plt.clf()


    #Define Plots
    ax1=fig.add_subplot(111)
    ax2 = fig.add_subplot(111, sharex=ax1, frameon=False)



    #Plot parameter setting
    if (mode.get()=='1'):#ch1 +ch2
        #print "Plotting Ch1+Ch2"
        YAxis_Text = "Ch1 Volts"
        Y2Axis_Text = "Ch2 Volts"
        XAxis_Text = Heading1
        ax1.yaxis.set_visible(True)
        ax2.yaxis.set_visible(True)
        canvas.get_tk_widget().place(width=640, height=480,x=50,y=100)
        DFT_Display = False
        #data
        x=data[0][1:201]
        y=data[1][1:201]
        y1=data[2][1:201]
        #axis
        v1=(0,((Data_Length) * Multiplier),-12.5/Ch1_Gain,12.5/Ch1_Gain)
        v2=(0,((Data_Length) * Multiplier),-12.5/Ch2_Gain,12.5/Ch2_Gain)
        v3=(0,((Data_Length) * Multiplier),-12.5/Ch2_Gain,12.5/Ch2_Gain)
        #Plot
        plotAx1()
        plotAx2()

    elif (mode.get()=='2'):#ch1
        #print "Plotting Ch1"
        YAxis_Text = "Ch1 Volts"
        XAxis_Text = Heading1
        ax1.yaxis.set_visible(True)
        ax2.yaxis.set_visible(False)
        ax2.xaxis.set_visible(False)
        canvas.get_tk_widget().place(width=640, height=480,x=50,y=100)
        DFT_Display = False
        #data
        x=data[0][1:201]
        y=data[1][1:201]
        #axis
        v1=(0,(Data_Length/2 * Multiplier),-12.5/Ch1_Gain,12.5/Ch1_Gain)
        v3=(0,(Data_Length/2 * Multiplier),-12.5/Ch2_Gain,12.5/Ch2_Gain)
        plotAx1()

    elif (mode.get()=='3'):#ch2
        #print "Plotting Ch2"
        YAxis_Text = "Ch2 Volts"
        XAxis_Text = Heading1
        ax1.yaxis.set_visible(True)
        ax2.yaxis.set_visible(False)
        ax2.xaxis.set_visible(False)
        canvas.get_tk_widget().place(width=640, height=480,x=50,y=100)
        DFT_Display = False
        #data
        x=data[0][1:201]
        y=data[2][1:201]
        #axis
        v1=(0,(Data_Length/2 * Multiplier),-12.5/Ch2_Gain,12.5/Ch2_Gain)
        v3=(0,(Data_Length/2 * Multiplier),-12.5/Ch2_Gain,12.5/Ch2_Gain)
        plotAx1()

    elif (mode.get()=='4'):#XY
        #print "Plotting XY"
        YAxis_Text = "Ch2 Volts"
        XAxis_Text = "Ch1 Volts"
        ax1.yaxis.set_visible(True)
        ax2.yaxis.set_visible(False)
        ax2.xaxis.set_visible(False)
        canvas.get_tk_widget().place(width=520, height=520,x=100,y=65)
        DFT_Display = False
        #data
        x=data[1][1:201]
        y=data[2][1:201]
        #axis
        v1=(-12./Ch1_Gain,12.5/Ch1_Gain,-12.5/Ch2_Gain,12.5/Ch2_Gain)
        v3=(-12./Ch1_Gain,12.5/Ch1_Gain,-12.5/Ch2_Gain,12.5/Ch2_Gain)
        plotAx1()

    elif (mode.get()=='5'):#Ch1 fft
        def myFFTch1():
            global maxCh1
            temp1= np.fft.fft(data[1][1:], n=800, axis=-1)#ch1 data
            temp2=abs(temp1)/200 #/Ch1_Gain
            for j in range(400):
                k=j+1
                data[4][k]=round(temp2[j],3) #ch1 fft data
            maxCh1= np.max(temp2)+.2

        #print "Plotting ch1fft"
        YAxis_Text = "Ch1 Volts"
        XAxis_Text = "Frequency Hz"
        ax1.yaxis.set_visible(True)
        ax2.yaxis.set_visible(False)
        ax2.xaxis.set_visible(False)
        canvas.get_tk_widget().place(width=640, height=480,x=50,y=100)
        DFT_Display = False
        myFFTch1()
        #data
        x=data[3][1:201]
        y=data[4][1:201]
        #axis
        v1=(0,(Max_Frequency/2),0,maxCh1)
        v3=(0,(Max_Frequency/2),0,maxCh1)
        plotAx1()

    elif (mode.get()=='6'):#Ch2 fft
        def myFFTch2():
            global maxCh2
            temp1= np.fft.fft(data[2][1:], n=800, axis=-1)#ch1 data
            temp2=abs(temp1)/200 #/Ch2_Gain
            for j in range(400):
                k=j+1
                data[5][k]=round(temp2[j],3) #ch1 fft data
            maxCh2=np.max(temp2)+.2
        #print "Plotting ch2fft"
        YAxis_Text = "Ch2 Volts"
        XAxis_Text = "Frequency Hz"
        ax1.yaxis.set_visible(True)
        ax2.yaxis.set_visible(False)
        ax2.xaxis.set_visible(False)
        canvas.get_tk_widget().place(width=640, height=480,x=50,y=100)
        DFT_Display = False
        myFFTch2()
        #data
        x=data[3][1:201]
        y=data[5][1:201]
        #axis
        v1=(0,(Max_Frequency/2),0,maxCh2)
        v3=(0,(Max_Frequency/2),0,maxCh2)
        plotAx1()

    #plot ax3
    ax3 = fig.add_subplot(111, frameon=False)
    plotAx3()

    writeLabels()
    canvas.draw()

def plotAx1():
    global ax1#,v1,x,y,y1
    global YAxis_Text,Y2Axis_Text,XAxis_Text

    if(overWrite==False):
        ax1.cla()

    # Add the first axes using subplot populated with data
    #ax1=fig.add_subplot(111)
    ax1.plot(x,y,'r')
    #ax1.plot(x,y1,'g')
    ax1.set_ylabel(YAxis_Text,fontsize=16)
    ax1.grid('on')
    ax1.axis((v1))
    #To modify ticklines
    for line in ax1.yaxis.get_ticklines():
    # line is a Line2D instance
        line.set_color('red')
        line.set_markersize(6)
        line.set_markeredgewidth(1)
    for line in ax1.xaxis.get_ticklines():
    # line is a Line2D instance
        line.set_color('black')
        line.set_markersize(6)
        line.set_markeredgewidth(1)

def plotAx2():
    global ax2#,v2
    global YAxis_Text,Y2Axis_Text,XAxis_Text

    if(overWrite==False):
        ax2.cla()

    # now, the second axes that shares the x-axis with the ax1
    #ax2 = fig.add_subplot(111, sharex=ax1, frameon=False)
    ax2.plot(x,y1,'b')
    ax2.yaxis.tick_right()
    ax2.yaxis.set_label_position("right")
    ax2.set_ylabel(Y2Axis_Text,fontsize=16)
    ax2.grid('on')
    ax2.axis((v2))
    #To modify ticklines
    for line in ax2.yaxis.get_ticklines():
    # line is a Line2D instance
        line.set_color('blue')
        line.set_markersize(6)
        line.set_markeredgewidth(1)


def plotAx3(): #for Cursor
    global ax3,cursorReqd
##    if(cursorReqd==True):
##        ax3.cla()

    #For cursor
    #ax3 = fig.add_subplot(111, frameon=False)
    #print v
    ax3.xaxis.set_visible(False)
    ax3.yaxis.set_visible(False)

    ax3.axis((v3))

    if(cursorReqd==True):
        myCursor()
        canvas = FigureCanvasTkAgg(fig, master=mainFrame)
        cursorReqd=False

def writeLabels():
    global ax2,ax1,XAxis_Text


    #common labels
    plt.title(plotTitle.get(),fontsize=16)
    #ax2.set_xlabel(XAxis_Text,fontsize=16)
    ax1.set_xlabel(XAxis_Text,fontsize=16)

    my_logo=ax1.text(.81, 0.03, 'Aj_Scope2', bbox=dict(facecolor='White', alpha=0.9),transform=ax3.transAxes)


#------------End of plot---------------------



#Start#--------------Connect to Com Ports --------------------------------

#--------Connect Frame Container

connectFrame = ttk.Labelframe(mainFrame,text='Connect')
connectFrame.place(width=310, height=55,x=15,y=12)

# ------ Get Com Ports Button
def getComPorts():
    global availablePorts
    list_temp=tuple(list_serial_ports())
    comPortNames.set(list_temp)
    availablePorts=list_temp

getPorts=ttk.Button(connectFrame,text="Check Ports" , command=getComPorts )
getPorts.place(width=80,height=28,x=2,y=2)

#----- Available Com Port Display Listbox

#-----Function list_serial_ports() and convert to Tuple
def list_serial_ports():
    # Windows
    if os.name == 'nt':
        # Scan for available ports.
        available = []
        for i in range(256):
            try:
                s = serial.Serial(i)
                available.append('COM'+str(i + 1))
                #available.append(i)
                s.close()
            except serial.SerialException:
                pass
        return available
    else:
        # Mac / Linux
        portNames=glob.glob('/dev/ttyS*') + glob.glob('/dev/ttyUSB*') \
        + glob.glob('/dev/ttyACM*') #+ glob.glob('/dev/serial/by-id/*')
        #return [port[0] for port in list_ports.comports()]
        #print name
        return portNames

#availablePorts= tuple(list_serial_ports())

#------ Com Port Available ListBox
#availablePorts=() # Initial Blank Values
availablePorts = () # Defination
mySer = serial.Serial(baudrate = 115200, timeout=5) #(timeout=5)
comPortNames = StringVar(value=availablePorts)
def getIndex(*args):
    global selectedPort
    global mySerial
    idxs = com_listbox.curselection()
    ports=availablePorts
    #print ports
    #print ports[0]
    idx = int(idxs[0])
    #print idx
    port=ports[idx]
    #print port
    if port[0]=='C':
        index=int(port[3:])

        #print 'Windows COM Port', index #, type(index)
        index=index-1
        try:
            mySer.port= index
            if mySer.isOpen()==False:
                mySer.open()
                testSerial()
        except Exception:
            sigGenAck.set ("Serial Port Exception")
    elif port[0]=='/':
        linuxPort=ports[idx]
        #print 'Linux Port' ,type(linuxPort)
        try:
            mySer.port=linuxPort
            print mySer.isOpen()
            if mySer.isOpen()==False:
                mySer.open()
                testSerial()
        except Exception:
            sigGenAck.set ("Serial Port Exception")
        print mySer.isOpen()
    else:
        sigGenAck.set ("No Serial Port Found")
        pass


com_listbox =Listbox(connectFrame, listvariable=comPortNames, highlightbackground='#7f9dd3',
    exportselection='false',activestyle='none', font=('Ariel', '8')) # works
#com_listbox =Listbox(mainFrame, listvariable=comPortNames,font=('Ariel', '9')) # works
com_listbox.place(width=80,height=32,x=85,y=0)
com_listbox.bind('<<ListboxSelect>>', getIndex)

#---- Add Scroll bar
com_scroll = Scrollbar( connectFrame, orient=VERTICAL, command=com_listbox.yview)
com_listbox.configure(yscrollcommand=com_scroll.set)
#com_listbox['yscrollcommand'] = com_scroll.set #also works
com_scroll.place(height=32,x=165,y=0)

#--------Open Selected Serial Port
mySer = serial.Serial(baudrate = 115200, timeout=0.5) #(timeout=5)
def testSerial():
#testCmd=[55,0,0,0,8,0]  # Gives 37 00 00 00 08 00 Basic Test Command
    cmdString="I" # "I" for Identify
#cmdString=''.join(map(chr,testCmd))
    mySer.write(cmdString)

    testRead=mySer.readline()
    if len(testRead) <=0:
        #print 'timeout'
        sigGenAck.set('Serial Port Timeout')
        mySer.close()
    else:
        #print 'Scope Response =', testRead
        sigGenAck.set(testRead[0:-1])


#-------- Message from Scope
# ------Message  label
sigGenAck=StringVar()
sigGenAck.set('Status Message')
Label(connectFrame, textvariable= sigGenAck,anchor=W ,background="#c0c0c0",foreground='black',
font=('Ariel', '9') ).place(width=110, height= 30, x=185, y=2)


#End#--------------Connect to Com Ports --------------------------------


#Start#--------------Display Mode Selection --------------------------------
#Mode Frame Container
modeFrame = ttk.Labelframe(mainFrame,text='Display Mode')
modeFrame.place(width=402, height=55,x=328,y=12)

#mode radio button
def modeRadiobutton():
    mode.get()

mode = StringVar()
mode.set('1') #initialisation
ttk.Radiobutton(modeFrame, text='CH1 & CH2',command=modeRadiobutton,
    variable=mode, value='1').place (x=5,y=5)
ttk.Radiobutton(modeFrame, text='CH1',command=modeRadiobutton,
    variable=mode, value='2').place (x=95,y=5)
ttk.Radiobutton(modeFrame, text='CH2', command=modeRadiobutton,
    variable=mode, value='3').place (x=145,y=5)
ttk.Radiobutton(modeFrame, text='XY',command=modeRadiobutton,
    variable=mode, value='4').place (x=195,y=5)
ttk.Radiobutton(modeFrame, text='DFT CH1',command=modeRadiobutton,
    variable=mode, value='5').place (x=235,y=5)
ttk.Radiobutton(modeFrame, text='DFT CH2', command=modeRadiobutton,
    variable=mode, value='6').place (x=315,y=5)

##ttk.Label(mainFrame, textvariable=mode).place (x=450,y=0)

#End#--------------Display Mode Selection --------------------------------

#Start#--------------Trigger Mode--------------------------------
#Trigger  Frame Container
trigFrame = ttk.Labelframe(mainFrame,text='Trigger Mode')
trigFrame.place(width=150, height=96,x=748,y=12)

#trig radio button
def trigRadiobutton():
    trigMode.get()

trigMode = StringVar()
trigMode.set('0') #initialisation
ttk.Radiobutton(trigFrame, text='Auto',command=modeRadiobutton,
    variable=trigMode, value='0').place (x=15,y=5)
ttk.Radiobutton(trigFrame, text='CH1',command=modeRadiobutton,
    variable=trigMode, value='1').place (x=15,y=25)
ttk.Radiobutton(trigFrame, text='CH2', command=modeRadiobutton,
    variable=trigMode, value='2').place (x=15,y=45)

#trig radio button
def trigRadiobutton():
    trigPol.get()

trigPol = StringVar()
trigPol.set('0') #initialisation
ttk.Radiobutton(trigFrame, text='L/H',command=modeRadiobutton,
    variable=trigPol, value='0').place (x=85,y=5)
ttk.Radiobutton(trigFrame, text='H/L',command=modeRadiobutton,
    variable=trigPol, value='1').place (x=85,y=25)


##ttk.Label(mainFrame, textvariable=trigMode).place (x=850,y=0)
##ttk.Label(mainFrame, textvariable=trigPol).place (x=875,y=0)

#End#--------------Trigger Mode --------------------------------


#Start#--------------Capture Mode --------------------------------
#Capture Mode Container
captureFrame = ttk.Labelframe(mainFrame,text='Capture Mode')
captureFrame.place(width=100, height=96,x=907,y=12)

#trig radio button
def captureRadiobutton():
    captureMode.get()

captureMode = StringVar()
captureMode.set('1') #initialisation
ttk.Radiobutton(captureFrame, text='SINGLE',command=modeRadiobutton,
    variable=captureMode, value='1').place (x=15,y=5)
ttk.Radiobutton(captureFrame, text='REPEAT',command=modeRadiobutton,
    variable=captureMode, value='2').place (x=15,y=25)
ttk.Radiobutton(captureFrame, text='STORE',command=modeRadiobutton,
    variable=captureMode, value='3').place (x=15,y=45)


##ttk.Label(mainFrame, textvariable=captureMode).place (x=990,y=0)


#End#--------------Capture Mode --------------------------------


#Start#-------------- Sampling Rate --------------------------------
#Sampling Rate Container
samplingFrame = ttk.Labelframe(mainFrame,text='Sampling Rate')
samplingFrame.place(width=260, height=90,x=748,y=125)

# ------ Sampling Rate Listbox
#ListBox
samplingRates=('0.05us/sample', '0.10us/sample', '0.20us/sample', '0.50us/sample',
 '1.00us/sample', '2.00us/sample', '5.00us/sample','10.0us/sample',
 '20.0us/sampl2', '50.us/sample','0.10ms/sample',
 '0.20ms/sampl2', '0.50ms/sample','1.00ms/sample',
  '2.00ms/sampl2', '5.00ms/sample','10.0ms/sample',
'20.0ms/sampl2', '50.0ms/sample','0.10s/sample')
rateIndexes=(20,19,18,17,16,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15)
rateNames=('20Msps(ETS)','10Msps(ETS)','5.0Msps(ETS)','2.0Msps(ETS)','1.0Msps(ETS)',
'500ksps','200ksps','100ksps','50ksps','20ksps','10ksps','5ksps','2ksps','1ksps',
'0.50ksps','0.20ksps','0.10ksps','50sps','20sps','10sps')
samplingNames = StringVar(value=samplingRates)
rateIndex=StringVar(value=rateIndexes)
rateValue=StringVar(value=rateNames)
rateIndex.set('1')
rateValue.set('500ksps')

def myFunction(*args):
    idxs = sampling_listbox.curselection()
    idx = int(idxs[0])
    rateIndex.set(rateIndexes[idx])
    rateValue.set(rateNames[idx])


sampling_listbox = Listbox(samplingFrame, listvariable=samplingNames,exportselection='false',
   activestyle='none', highlightbackground='#7f9dd3', font=('Ariel', '9') ) #, works
sampling_listbox.place(width=120, height= 55, x=15, y=5)
sampling_listbox.bind('<<ListboxSelect>>', myFunction)
sampling_listbox.selection_set(5) #initialisation
#sampling_listbox.yview ('moveto', '0.5')
sampling_listbox.yview('scroll', '4', 'units')

#---- Add Scroll bar
sampleScroll = Scrollbar( samplingFrame, orient=VERTICAL, command=sampling_listbox.yview)
#sampling_listbox.configure(yscrollcommand=sampleScroll.set)#works
sampling_listbox['yscrollcommand'] = sampleScroll.set #also works
#sampleScroll.set(.5,.5)
sampleScroll.place(height=55,x=130,y=5)

ttk.Label(samplingFrame, textvariable=rateValue, background='#ece9d8',
font=('Ariel', '10')).place (x=155,y=22)

##ttk.Label(mainFrame, textvariable=rateIndex).place (x=900,y=113)


#End#----------------Sample Rate--------------------------------


#Start#----------------Gain Setting--------------------------------
#CH1 Gain Container
ch1gainFrame = ttk.Labelframe(mainFrame,text='CH1 Gain')
ch1gainFrame.place(width=125, height=85,x=748,y=230)

#CH1 gain radio button
def ch1gainRadiobutton():
    ch1gain.get()


ch1gain = StringVar()
ch1gain.set('0') #initialisation
ttk.Radiobutton(ch1gainFrame, text='GAIN 1',command=modeRadiobutton,
    variable=ch1gain, value='0').place (x=25,y=0)
ttk.Radiobutton(ch1gainFrame, text='GAIN 2',command=modeRadiobutton,
    variable=ch1gain, value='1').place (x=25,y=20)
ttk.Radiobutton(ch1gainFrame, text='GAIN 5',command=modeRadiobutton,
    variable=ch1gain, value='3').place (x=25,y=40)


##ttk.Label(mainFrame, textvariable=ch1gain).place (x=825,y=215)

#CH2 Gain Container
ch2gainFrame = ttk.Labelframe(mainFrame,text='CH2 Gain')
ch2gainFrame.place(width=125, height=85,x=882,y=230)

#CH1 gain radio button
def ch1gainRadiobutton():
    ch2gain.get()

ch2gain = StringVar()
ch2gain.set('0') #initialisation
ttk.Radiobutton(ch2gainFrame, text='GAIN 1',command=modeRadiobutton,
    variable=ch2gain, value='0').place (x=25,y=0)
ttk.Radiobutton(ch2gainFrame, text='GAIN 2',command=modeRadiobutton,
    variable=ch2gain, value='1').place (x=25,y=20)
ttk.Radiobutton(ch2gainFrame, text='GAIN 5',command=modeRadiobutton,
    variable=ch2gain, value='3').place (x=25,y=40)


##ttk.Label(mainFrame, textvariable=ch2gain).place (x=950,y=215)

#End#----------------Gain Setting----------------------------------------


#Start#----------------Offset and Trigger Setting-------------------------
#Ch1Offset Container------------------------------------------------------
ch1offsetFrame = ttk.Labelframe(mainFrame,text='CH1 Offset')
ch1offsetFrame.place(width=80, height=250,x=748,y=330)
#Scale
def ch1offsetValue(*args):
    temp=(ch1valueD.get()) # () is very important
    temp =(temp *12.5/512.) -12.5
    temp1=temp*Scale_Factor #reducing the value effectively makes us set more
    temp1=round(temp1,2)
    #temp=int(temp)
    ch1value.set(temp1)
    #print temp

ch1valueD=DoubleVar()
ch1value=StringVar()
ch1value.set('0.00')
ch1valueD.set('512')
ch1offsetScale = ttk.Scale(ch1offsetFrame, command=ch1offsetValue,
orient=VERTICAL, variable=ch1valueD,length=200,
from_=1024., to=0.).place(x=20,y=5)

ttk.Label(ch1offsetFrame, textvariable= ch1value).place(x=20,y=205)
#Ch2Offset Container------------------------------------------------------
ch2offsetFrame = ttk.Labelframe(mainFrame,text='CH2 Offset')
ch2offsetFrame.place(width=80, height=250,x=837,y=330)
#Scale
def ch2offsetValue(*args):
    temp=(ch2valueD.get()) # () is very important
    temp =(temp *12.5/512.) -12.5
    temp1=temp*Scale_Factor #reducing the value effectively makes us set more
    temp1=round(temp1,2)
    ch2value.set(temp1)
    #print temp

ch2valueD=DoubleVar()
ch2value=StringVar()
ch2value.set('0.00')
ch2valueD.set('512')
ch2offsetScale = ttk.Scale(ch2offsetFrame, command=ch2offsetValue,
orient=VERTICAL, variable=ch2valueD,length=200,
from_=1024., to=0.).place(x=20,y=5)

ttk.Label(ch2offsetFrame, textvariable= ch2value).place(x=20,y=205)

#Trigger Level Container------------------------------------------------------
triggerFrame = ttk.Labelframe(mainFrame,text='Trig. Level')
triggerFrame.place(width=80, height=250,x=926,y=330)
#Scale
def triggerValue(*args):
    Setup1_Gains()
    temp=(trigValueD.get()) # () is very important
    temp=round(temp,0)
##    if (trigMode.get()=='1'): #Trig by CH1
##        temp =(temp *(12.5/Ch1_Gain)/512.) -12.5/Ch1_Gain
##    elif (trigMode.get()=='2'): #Trig by Ch2
##        temp =(temp *(12.5/Ch2_Gain)/512.) -12.5/Ch2_Gain
##    else:
##        temp=0.00 #Not applicable

    temp =temp *(12.5/512.) -12.5

    temp1=temp*Scale_Factor
    temp1=round(temp1,2)
    trigValue.set(temp1)
    #print temp

trigValueD=DoubleVar()
trigValue=StringVar()
trigValue.set('0.00')
trigValueD.set('512')
triggerScale =ttk.Scale(triggerFrame, command=triggerValue,
orient=VERTICAL, variable=trigValueD,length=200,
from_=1024., to=0.).place(x=20,y=5)

ttk.Label(triggerFrame, textvariable= trigValue).place(x=20,y=205)
#End#----------------Offset and Trigger Setting----------------------------





#Start -------------Setup------------------------------------



#my asc112int routine
def ascii2int(myAscii):
    ans1=hex(ord(myAscii))
    ans=int(ans1,16)
    return ans

#Gain, Trig Source and Polarity, Sampling Rate
def Setup1_Gains():#Set the channel gains with scale factor correction
    global Ch1_Gain,Ch2_Gain
#getting the channel gains
    if (ch1gain.get()=='0'):
         Ch1_Gain = 1.
    elif (ch1gain.get()=='1'):
         Ch1_Gain = 2.
    elif (ch1gain.get()=='3'):
         Ch1_Gain = 5.

    if (ch2gain.get()=='0'):
         Ch2_Gain = 1.
    elif (ch2gain.get()=='1'):
         Ch2_Gain = 2.
    elif (ch2gain.get()=='3'):
         Ch2_Gain = 5.




def Setup2_TrigModePolSampRate():
#Setting the command string for gain,trig mode,trig polarity and Sampling rate
    setGain= [71,int(ch1gain.get()),int(ch2gain.get())] #Gain
    setTrig= [84,int(trigMode.get())] #Trig Source
    setTrigPol=[80,int( trigPol.get())] #Trig Polarity
    setSampling=[83,int( rateIndex.get())] #Sampling Rate

    #print 'rateIndex= ',rateIndex.get()


    cmdString=''.join(map(chr,(setGain)))# +setTrig+setTrigPol+setSampling)))
    mySer.write(cmdString)
    sleep(0.01)
    cmdString=''.join(map(chr,(setTrig)))
    mySer.write(cmdString)
    sleep(0.01)
    cmdString=''.join(map(chr,(setTrigPol)))
    mySer.write(cmdString)
    sleep(0.01)
    cmdString=''.join(map(chr,(setSampling)))
    mySer.write(cmdString)
    sleep(0.01)

    #print str(cmdString)

#Reading the reference voltage and calculating  Scale_factor
def Setup3_ScaleFactor():
    global Scale_Factor
    readVref=[82]
    cmdString=''.join(map(chr,(readVref)))

    mySer.flushInput()#clearing the buffer
    mySer.write(cmdString)
    #sleep(0.01)
    while(mySer.inWaiting()<1):()
    readString=mySer.read(6) #returns bytes upto timeout

    Vref_array= map(ascii2int,readString)
   # print Vref_array
    #'First Byte is 'R' then AN2 msb-lsb /AN3 msb-lsb
    Vref = Vref_array[-4]*256+Vref_array[-3]+Vref_array[-2]*256+Vref_array[-1]
    Vref = Vref/2 #'Average of AN2 & AN3

    Scale_Factor= round(673.0/Vref, 3)# 673=1024* (3.29/5)
    #print 'Vref=',Vref , 'Scale Factor =', Scale_Factor


def Setup4_Offset_Trigger():#Zero Offset & Trigger values
    Offset_Gain_Factor,Ch1_Zero_Offset,Ch2_Zero_Offset=float(),float(),float()
    #Based on the input resistor divider independent of VDD
    Offset_Gain_Factor = (1.25) # / Scale_Factor#  '1.24 experimentally instead of 1.25
    #Offset_Gain_Factor = (5 / 4) / Scale_Factor

    #These are the Zero Offset Values
    #Not affected by scale factor
    Ch1_Zero_Offset = (512 *  Offset_Gain_Factor) / Ch1_Gain
    Ch2_Zero_Offset = (512 *  Offset_Gain_Factor) / Ch2_Gain

    #Additional offset to shift the display by the manual offset values
    #Based on ch1valueD.get and ch2valueD.get
    #1024/0 are the max / min values
    #Seems Independent of Gain

    Ch1_Zero_Offset  = Ch1_Zero_Offset + (ch1valueD.get()- 512)* Offset_Gain_Factor
    Ch2_Zero_Offset  = Ch2_Zero_Offset + (ch2valueD.get()- 512)* Offset_Gain_Factor

    #print 'ch2valueD.get()=' , ch2valueD.get()

    #Limiting the values to 10bit
    if (Ch1_Zero_Offset <= 0):
        Ch1_Zero_Offset = 0
    elif (Ch1_Zero_Offset >= 1024):
        Ch1_Zero_Offset = 1023

    if (Ch2_Zero_Offset <= 0):
        Ch2_Zero_Offset = 0
    elif (Ch2_Zero_Offset >= 1024):
        Ch2_Zero_Offset = 1023

    #print Ch1_Zero_Offset,Ch2_Zero_Offset , trigValueD.get()#Final values

    ch1_offset_cmd=[79,2,256] #dummy defination
    ch1_offset_cmd[1]= int(math.floor( Ch1_Zero_Offset / 256))
    ch1_offset_cmd[2] = int(Ch1_Zero_Offset -  ch1_offset_cmd[1] * 256)

    ch2_offset_cmd=[111,2,256] #dummy defination
    ch2_offset_cmd[1]= int(math.floor( Ch2_Zero_Offset / 256))
    ch2_offset_cmd[2] = int(Ch2_Zero_Offset -  ch2_offset_cmd[1] * 256)

    #Trig Value
    trig_value_cmd=[76,2,256] #dummy defination

    temp=(trigValueD.get()-512.)

    if (trigMode.get()=='1'): #Trig by CH1
        temp =(temp *Ch1_Gain) + 512
    elif (trigMode.get()=='2'): #Trig by Ch2
       temp =(temp *Ch2_Gain) + 512
    else:
        temp=512 #Not applicable

    if (temp<= 0):
        temp = 0
    elif (temp >= 1024):
        temp = 1023

    trig_value_cmd[1]= int(math.floor( (temp) / 256))
    trig_value_cmd[2] = int((temp) -  trig_value_cmd[1] * 256)


    #print str(ch1_offset_cmd),str(ch2_offset_cmd),str(trig_value_cmd)


    cmdString=''.join(map(chr,(ch1_offset_cmd)))# Set Ch1 offset
    mySer.write(cmdString)
    #print 'ch1=',cmdString
    sleep(0.01)
    cmdString=''.join(map(chr,(ch2_offset_cmd)))# Set Ch2 offset
    #print 'ch2=',cmdString
    mySer.write(cmdString)
    sleep(0.01)
    cmdString=''.join(map(chr,(trig_value_cmd)))# Set Trig Value
    mySer.write(cmdString)
    sleep(0.01)

    mySer.flushInput()#clearing the buffer

def Setup5_SelectMode(): #DataLength,DFT,ReadMode
    global v1,v2,v3,Ch1_Gain,Ch2_Gain,ax1on,ax2on,ax3on
    #mode 1/2/3/4/5/6   ch1+ch2/ch1/ch2/XY/dftch1/dftch2
    global Data_Length,DFT,Read_Mode

    if (mode.get()=='1'):#ch1 +ch2
        Data_Length = 200
        DFT = False
        Read_Mode = '1' #200 CH1 "200 CH2
        fftCmd=[70,0 ] #Normal
    elif (mode.get()=='2'):#ch1
        Data_Length = 400
        DFT = False
        Read_Mode = '2' #200 CH1 + 200 CH1 based on fftCmd
        fftCmd=[70,1 ] #Ch1
    elif (mode.get()=='3'):#ch2
        Data_Length = 400
        DFT = False
        Read_Mode = '3' #200 CH2 + 200 CH2 based on fftCmd
        fftCmd=[70,2 ] #Ch2
    elif (mode.get()=='4'):#XY
        Data_Length = 200
        DFT = False
        Read_Mode = '1' #200 CH1 "200 CH2
        fftCmd=[70,0 ] #Normal
    elif (mode.get()=='5'):#dft ch1
        Data_Length = 400
        DFT = True
        Read_Mode = '2' #200 CH1 + 200 CH1 based on fftCmd
        fftCmd=[70,1 ] #Ch1
    elif (mode.get()=='6'):#dft ch2
        Data_Length = 400
        DFT = True
        Read_Mode = '3' #200 CH2+ 200 CH2 based on fftCmd
        fftCmd=[70,2 ] #Ch2

    cmdString=''.join(map(chr,(fftCmd)))# Write FFT mode command
    mySer.write(cmdString)
    sleep(0.01)

def ETS_Error_Message():
   tkMessageBox.showinfo("Error", "No ETS in Auto Mode")

def Setup6_SamplingRate():
    global  ETS,ETS_Error,Heading1,Multiplier,Max_Frequency
##samplingRates=('0.05us/sample', '0.10us/sample', '0.20us/sample', '0.50us/sample',
## '1.00us/sample', '2.00us/sample', '5.00us/sample','10.0us/sample',
## '20.0us/sampl2', '50.us/sample','0.10ms/sample',
## '0.20ms/sampl2', '0.50ms/sample','1.00ms/sample',
##  '2.00ms/sampl2', '5.00ms/sample','10.0ms/sample',
##'20.0ms/sampl2', '50.0ms/sample','0.10s/sample')

##rateNames=('20Msps(ETS)','10Msps(ETS)','5.0Msps(ETS)','2.0Msps(ETS)','1.0Msps(ETS)',
##'500ksps','200ksps','100ksps','50ksps','20ksps','10ksps','5ksps','2ksps','1ksps',
##'0.50ksps','0.20ksps','0.10ksps','50sps','20sps','10sps')

##rateIndexes=(20,19,18,17,16,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15)

#'20Msps(ETS)'
    if (rateIndex.get()=='20'):
        ETS = 1
        ETS_Error = 0
        if (trigMode.get()=='0'): #Auto_Trig
            ETS_Error_Message()
            stop()
            ETS_Error = 1
        Heading1 = "Time(uSec)"
        Multiplier =  0.05
        Max_Frequency = 10000000.
#'10Msps(ETS)'
    elif (rateIndex.get()=='19'):
        ETS = 1
        ETS_Error = 0
        if (trigMode.get()=='0'): #Auto_Trig
            ETS_Error_Message()
            stop()
            ETS_Error = 1
        Heading1 = "Time(uSec)"
        Multiplier = 0.1
        Max_Frequency = 5000000.
#'5.0Msps(ETS)'
    elif (rateIndex.get()=='18'):
        ETS = 1
        ETS_Error = 0
        if (trigMode.get()=='0'): #Auto_Trig
            ETS_Error_Message()
            stop()
            ETS_Error = 1
        Heading1 = "Time(uSec)"
        Multiplier = 0.2
        Max_Frequency = 2500000.
#'2.0Msps(ETS)'
    elif (rateIndex.get()=='17'):
        ETS = 1
        ETS_Error = 0
        if (trigMode.get()=='0'): #Auto_Trig
            ETS_Error_Message()
            stop()
            ETS_Error = 1
        Heading1 = "Time(uSec)"
        Multiplier = 0.5
        Max_Frequency = 1000000.
#'1.0Msps(ETS)'
    elif (rateIndex.get()=='16'):
        ETS = 1
        ETS_Error = 0
        if (trigMode.get()=='0'): #Auto_Trig
            ETS_Error_Message()
            stop()
            ETS_Error = 1
        Heading1 = "Time(uSec)"
        Multiplier = 1.0
        Max_Frequency = 500000.

#---------Non ETS---------------------------
#'500ksps'
    elif (rateIndex.get()=='1'):
        ETS = 0
        ETS_Error = 0
        Heading1 = "Time(uSec)"
        Multiplier = 2.0
        Max_Frequency = 250000.
#'200ksps'
    elif (rateIndex.get()=='2'):
        ETS = 0
        ETS_Error = 0
        Heading1 = "Time(uSec)"
        Multiplier = 5.
        Max_Frequency = 100000.
#'100ksps'
    elif (rateIndex.get()=='3'):
        ETS = 0
        ETS_Error = 0
        Heading1 = "Time(uSec)"
        Multiplier = 10.
        Max_Frequency = 50000.
#'50ksps'
    elif (rateIndex.get()=='4'):
        ETS = 0
        ETS_Error = 0
        Heading1 = "Time(uSec)"
        Multiplier = 20.
        Max_Frequency = 25000.
#'20ksps'
    elif (rateIndex.get()=='5'):
        ETS = 0
        ETS_Error = 0
        Heading1 = "Time(uSec)"
        Multiplier = 50.
        Max_Frequency = 10000.
#'10ksps'
    elif (rateIndex.get()=='6'):
        ETS = 0
        ETS_Error = 0
        Heading1 = "Time(mSec)"
        Multiplier = 0.1
        Max_Frequency = 5000.
#'5ksps'
    elif (rateIndex.get()=='7'):
        ETS = 0
        ETS_Error = 0
        Heading1 = "Time(mSec)"
        Multiplier = 0.2
        Max_Frequency = 2500.
#'2ksps'
    elif (rateIndex.get()=='8'):
        ETS = 0
        ETS_Error = 0
        Heading1 = "Time(mSec)"
        Multiplier = 0.5
        Max_Frequency = 1000.
#'1ksps'
    elif (rateIndex.get()=='9'):
        ETS = 0
        ETS_Error = 0
        Heading1 = "Time(mSec)"
        Multiplier = 1.
        Max_Frequency = 500.
#'0.50ksps'
    elif (rateIndex.get()=='10'):
        ETS = 0
        ETS_Error = 0
        Heading1 = "Time(mSec)"
        Multiplier = 2.
        Max_Frequency = 250.
#'0.20ksps'
    elif (rateIndex.get()=='11'):
        ETS = 0
        ETS_Error = 0
        Heading1 = "Time(mSec)"
        Multiplier = 5.
        Max_Frequency = 100.
#'0.10ksps'
    elif (rateIndex.get()=='12'):
        ETS = 0
        ETS_Error = 0
        Heading1 = "Time(mSec)"
        Multiplier = 10.
        Max_Frequency = 50.
#'50sps'
    elif (rateIndex.get()=='13'):
        ETS = 0
        ETS_Error = 0
        Heading1 = "Time(mSec)"
        Multiplier = 20.
        Max_Frequency = 25.
#'20sps'
    elif (rateIndex.get()=='14'):
        ETS = 0
        ETS_Error = 0
        Heading1 = "Time(mSec)"
        Multiplier = 50.
        Max_Frequency = 10
#'10sps'
    elif (rateIndex.get()=='15'):
        ETS = 0
        ETS_Error = 0
        Heading1 = "Time(Sec)"
        Multiplier = 0.1
        Max_Frequency = 5.


#End-----------------Setup---------------------------------

#Start ---------------Run----------------------------------
#Capture and read data
def capture_data():
    global keepRunning,Multiplier,Read_Mode,Data_Length
    #keepRunning=True


    if (Read_Mode =='1'):
        #print 'Read_Mode =', Read_Mode

        capture_cmd=[67] #C 01
        cmdString=''.join(map(chr,( capture_cmd)))# +setTrig+setTrigPol+setSampling)))
        mySer.flushInput()#clearing the buffer
        mySer.write(cmdString)
        sleep(0.02)
        while(mySer.inWaiting()<1):()
        readString=mySer.read(5) #returns bytes upto timeout
        #print readString #Done
        mySer.flushInput()#clearing the buffer

        #read 200 ch1 data
        read_ch1=[68,1] #D 01
        cmdString=''.join(map(chr,( read_ch1)))
        mySer.write(cmdString)
        sleep(0.02)
        while(mySer.inWaiting()<1):()
        readString=mySer.read(200)
        #print readString
        first_data= map(ascii2int,readString)
        #print ch1_data

        #read 200 ch2 data
        read_ch2=[68,2] #D 02
        cmdString=''.join(map(chr,( read_ch2)))#
        mySer.write(cmdString)
        while(mySer.inWaiting()<1):()
        readString=mySer.read(200)
        #print readString
        second_data= map(ascii2int,readString)
        #print ch2_data

    elif (Read_Mode =='2')or (Read_Mode =='3') :
        #print 'Read_Mode =', Read_Mode

        capture_cmd=[67] #C 01
        cmdString=''.join(map(chr,( capture_cmd)))# +setTrig+setTrigPol+setSampling)))
        mySer.flushInput()#clearing the buffer
        mySer.write(cmdString)
        sleep(0.02)
        while(mySer.inWaiting()<1):()
        readString=mySer.read(5) #returns bytes upto timeout
        #print readString #Done
        mySer.flushInput()#clearing the buffer

        #read 200 ch1 /ch2 data into ch1_data
        read_ch1=[68,3] #D 03
        cmdString=''.join(map(chr,( read_ch1)))
        mySer.write(cmdString)
        sleep(0.02)
        while(mySer.inWaiting()<1):()
        readString=mySer.read(200)
        #print readString
        first_data= map(ascii2int,readString)
        #print ch1_data

        #read second 200 ch1 /ch2  data into ch2_data
        read_ch2=[68,4] #D 04
        cmdString=''.join(map(chr,( read_ch2)))#
        mySer.write(cmdString)
        while(mySer.inWaiting()<1):()
        readString=mySer.read(200)
        #print readString
        second_data= map(ascii2int,readString)
        #print ch2_data

    #--------Transfer Data -------------
    if (Read_Mode =='1'): #ch1 200 + ch2 200
        for j in range(200):
            k=j+1
            temp=first_data[j]*(12./128.) -12.
            temp=temp*Scale_Factor/Ch1_Gain
            data[1][k]=round(temp,3)
            #x[j]=j

        for j in range(200):
            k=j+1
            temp=second_data[j]*(12./128.) -12.
            temp=temp*Scale_Factor/Ch2_Gain
            data[2][k]=round(temp,3)

    elif (Read_Mode =='2'): #ch1 200 + ch1 200
        for j in range(200):
            k=j+1
            temp=first_data[j]*(12./128.) -12.
            temp=temp*Scale_Factor/Ch1_Gain
            data[1][k]=round(temp,3)
            #x[j]=j

        for j in range(200):
            k=j+201
            temp=second_data[j]*(12./128.) -12. #actually second ch1 data
            temp=temp*Scale_Factor/Ch1_Gain
            data[1][k]=round(temp,3)


    elif (Read_Mode =='3'): #ch2 200 + ch2 200

        for j in range(200):
            k=j+1
            temp=first_data[j]*(12./128.) -12.
            temp=round(temp,3)
            temp=temp*Scale_Factor/Ch2_Gain
            data[2][k]=round(temp,3)

        for j in range(200):
            k=j+201
            temp=second_data[j]*(12./128.) -12.
            temp=temp*Scale_Factor/Ch2_Gain
            data[2][k]=round(temp,3)

    #'Time / Sample and Frequency
    MaxFreq_by_DataLength = (Max_Frequency / (Data_Length))
    for j in range(Data_Length):
        k=j+1
        data[0][k]=j* Multiplier # 'Time per sample [j] #first row
        data[3][k]=j* MaxFreq_by_DataLength  # 'Freq/sample[j] #fourth row


#Save to file
def saveCSV():
    global data
    #data[0]=['Time','CH1 Data','CH2 Data'] #Header
    my_file = tkFileDialog.asksaveasfilename(defaultextension='.csv',filetypes=[('csv files', '.csv')])
    #interchange rows and columns
    data1= zip(*data) #transposes the data

    b = open(my_file, 'wb') # 'wb' is important 'w' gives alternalelines in excel
    a = csv.writer(b)
    a.writerows(data1)
    b.close()
#End------------------Run----------------------------------



def Setup():
    #mainFrame.update()
    mySer.flushInput()#clearing the buffer
    #Setup5()#moderadiobutton Axis Setting and Plot Selection
    Setup2_TrigModePolSampRate()#Gain, Trig Source and Polarity, Sampling Rate Cmd String
    Setup3_ScaleFactor()#Reading the reference voltage and calculating  Scale_factor
    Setup1_Gains()#Setting the ch gains no scale factor correction
    Setup4_Offset_Trigger()#Setting the offset PWM for Ch1 and Ch2
    Setup5_SelectMode()#DataLength,DFT,ReadMode
    Setup6_SamplingRate()# ETS_Error,Multiplier,Heading1
    #print "setup done"

def run():
    global keepRunning,runStatus,overWrite,ETS_Error

    keepRunning==True
    #print 'keepRunning =', keepRunning
    #print 'button text=', myRun.config('text')[-1]
    myStop.configure(text="STOP")
    if (myRun.config('text')[-1]== 'RUN'):
        #"BUSY" Then 'Avoid problem when RUN button is pressed when Busy
        #No Run with ETS error

        while(True):
            myRun.configure(text="BUSY")
            #runStatus == True # Busy
            #print 'keepRunning,overWrite=', keepRunning, overWrite
            if (keepRunning==False):
                #print 'breaking'
                break
            #Check for Capture method Single, Continuous, Storage
            #print 'captureMode=' , captureMode.get()
            if (captureMode.get()=='1'): #Single
                keepRunning = False
                overWrite = False
            elif(captureMode.get()=='2'): #Repeat
                keepRunning = True #'Till Stopped
                overWrite = False
            elif(captureMode.get()=='3'): #Store
                keepRunning = False #'Till Stopped
                overWrite = True

            Setup()

            #print 'ETS_Error=' , ETS_Error
            if(ETS_Error==0): #No Capture with ETS error
                #print 'capturing'
                capture_data()
                #print "Data Captured"
                plot()
##                print 'keepRunning,overWrite=', keepRunning, overWrite
            if (keepRunning==False):
                #print 'breaking'
                break

            mainFrame.update()#Similar to Application.DoEvents()
            #print 'keepRunning,overWrite=', keepRunning, overWrite

        #print 'stopped running'
        myRun.configure(text='RUN')
        #runStatus == False # Not Busy
        myStop.configure(text="DONE")

    #print 'Exit Run'
    keepRunning=True #Required as something funny with break

def stop():
    global keepRunning
    #abort()
    keepRunning=False
    myRun.configure(text="RUN")

#------------------Buttons Setup Run Stop Save ---------------------------------



##mySetup=ttk.Button(mainFrame,text="SETUP" , command=Setup )
##mySetup.place(width=75,height=37,x=660,y=600)

myRun=ttk.Button(mainFrame,text="RUN" , command=run )
myRun.place(width=75,height=37,x=750,y=600)

myStop=ttk.Button(mainFrame,text="STOP" , command=stop )
myStop.place(width=75,height=37,x=840,y=600)

##mySave=ttk.Button(mainFrame,text="Save" , command=saveCSV )
##mySave.place(width=60,height=25,x=200,y=650)

#----Some Temp Stuff

#-----------Generate data to be plotted--------
#--Data now from Hardware
plotTitle=StringVar()
plotTitle.set("             Enter Plot Title")
myTitle= ttk.Entry(mainFrame, textvariable=plotTitle,font=('Ariel', '12'))
myTitle.place(width=250, height=25,x=300,y=605)


mySave=ttk.Button(mainFrame,text="Save" , command=saveCSV )
mySave.place(width=75, height=37,x=930,y=600)

##def abort():
##    cmdString='A'
##    if(mySer.isOpen()==True):
##        mySer.write(cmdString)
##
##myAbort=ttk.Button(mainFrame,text="Abort" , command=abort )
##myAbort.place(width=75, height=37,x=570,y=600)

##def Store():
##    my_data_file = tkFileDialog.asksaveasfilename(defaultextension='.npy',filetypes=[('data files', '.npy')])
##    np.save(my_data_file, data)
##myStore=ttk.Button(mainFrame,text="Store" , command=Store )
##myStore.place(width=60,height=25,x=125,y=650)
##
##def Load():
##    global data
##    my_data_file = tkFileDialog.askopenfilename(filetypes=[('data files', '.npy')])
##    data = np.load(my_data_file)
##myLoad=ttk.Button(mainFrame,text="Load" , command=Load )
##myLoad.place(width=60,height=25,x=50,y=650)

#--------Exit
def cleanExit():
    stop()
    mySer.close()
    mainFrame.quit()
    mainFrame.destroy()

myExit=ttk.Button(mainFrame,text="EXIT" , command=cleanExit )
myExit.place(width=75,height=25,x=840,y=650)

mainFrame.mainloop()

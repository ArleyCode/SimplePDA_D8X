// BluetoothLibSampleVC.cpp : Defines the entry point for the application.
//
//  This is sample program for IT-600 ot DT-X11 BlueTooth library
//  When you want to execute this program, you should set Bluetooth printer address like that

//		TCHAR	PrinterAdr[]=TEXT("00:80:37:17:78:DA");
//  We confirmed this program by using EXTECH MODEL 3500L Bluetooth printer and Zebra QL320
//  Bluetooth printer.
//


#include	"stdafx.h"
#include	"BluetoothLib.h"
#include	"SystemLib.h"

// Global Variables:
HCURSOR		hCursor;
HANDLE		hSerial=INVALID_HANDLE_VALUE;

// Forward declarations of functions included in this code module:
void		WaitCurON		(void);
void		WaitCurOFF		(void);
// Serial Port prototypes
HANDLE		PortOpen		(LPTSTR,DWORD, BYTE, BYTE, BYTE, DWORD, DWORD);
void		PortTimeouts	(DWORD, DWORD, HANDLE);
DWORD		PortWrite		(char *, DWORD, HANDLE);
void		PortClose		(HANDLE);


TCHAR		LocalName[]=TEXT("Terminal001");
TCHAR		PrinterAdr[]=TEXT("00:80:37:17:56:2D");

char		label1[]="***** TEST PRINT *****\n\r" \
							"IT-600 or DT-X11\n\r" \
							"CASIO\n\r" \
							"www.casio.co.jp/English/system\n\r" \
							"\n\r" \
							"CASIO Computer Co., Ltd.\n\r" \
							"\n\r" \
							"6-2, Hon-machi 1-chome\n\r" \
							"\n\r" \
							"Shibuya-ku, Tokyo 151-8543,Japan\n\r" \
							"\n\r" \
							"Tel.: +81 (3) 5334 4771\n\r" \
							"Fax.: +81 (3) 5334 4656\n\r" \
							"\n\r" \
							"\n\r" \
							"\n\r" \
							"\n\r" \
							"\n\r" \
							"\n\r";


int WINAPI WinMain(	HINSTANCE hInstance,
					HINSTANCE hPrevInstance,
					LPTSTR    lpCmdLine,
					int       nCmdShow)
{
 	// TODO: Place code here.
	BTST_LOCALINFO		bt_li;		// local device info structure

	BTST_DEVICEINFO		bt_di[BTDEF_MAX_UUID_NUMBER + 1];	// remote device info structure

	DWORD				bt_dmax;		// maximum number of bt-devices for inquiry
	DWORD				ii = 0;
	DWORD				BTret;		// bluetooth library return code
	BOOL				PrinterFound;
	DWORD				dwCommStatus;
	DWORD				i = 0;

	// initialize bluetooth device (power on)
	WaitCurON();
	BTret=BTInitialize();
	WaitCurOFF();
	if(BTret != BTERR_SUCCESS){
		MessageBox(NULL, TEXT("BT init error!"), TEXT("ERROR"), MB_OK | MB_ICONWARNING | MB_SETFOREGROUND);
		return(-1);
	}
	// get local device info
	BTret=BTGetLocalInfo(&bt_li);
	if(BTret != BTERR_SUCCESS){
		MessageBox(NULL, TEXT("BT get local info error!"), TEXT("ERROR"), MB_OK | MB_ICONWARNING | MB_SETFOREGROUND);
		BTDeInitialize();		// deinitialize bt device (power off)
		return(-2);
	}
	// set fixed values for our local bt device
	// (can be skiped if already done before)
	wcscpy(bt_li.LocalName, LocalName);

	bt_li.LocalDeviceMode=BTMODE_BOTH_ENABLED;

	bt_li.Authentication=FALSE;

	bt_li.Encryption=FALSE;
	BTret=BTSetLocalInfo(&bt_li);
	if(BTret != BTERR_SUCCESS){
		MessageBox(NULL, TEXT("BT set local info error!"), TEXT("ERROR"), MB_OK | MB_ICONWARNING | MB_SETFOREGROUND);
		BTDeInitialize();		// deinitialize bt device (power off)
		return(-4);
	}

	BTret=BTRegisterLocalInfo();

	if(BTret != BTERR_SUCCESS){
		MessageBox(NULL, TEXT("BT register local info error!"), TEXT("ERROR"), MB_OK | MB_ICONWARNING | MB_SETFOREGROUND);
		BTDeInitialize();		// deinitialize bt device (power off)
		return(-3);
	}
	// search for availible bluetooth devices
	bt_dmax=BTDEF_MAX_UUID_NUMBER;
	WaitCurON();

	BTret=BTInquiry(NULL, &bt_dmax, 5000); //5 second execute inquiry job

	WaitCurOFF();
	if(BTret != BTERR_SUCCESS){
		MessageBox(NULL, TEXT("BT Inquiry error!"), TEXT("ERROR"), MB_OK | MB_ICONWARNING | MB_SETFOREGROUND);
		BTDeInitialize();		// deinitialize bt device (power off)
		return(-6);
	}
	
	// get device details and try to find the specified bluetooth printer
	PrinterFound=FALSE;

	if((BTret=BTGetDeviceInfo(bt_di, bt_dmax, 0)) == BTERR_SUCCESS){

		for (i=0; i< bt_dmax; i++){

			if(wcscmp(bt_di[i].DeviceAddress, PrinterAdr) == 0){
				// we found the printer and try to get service informations
				// (can be skiped because we know printer capabilities)
				BTGetServiceInfo(&bt_di[i]);
				// register this device in registry
				// (can be skiped if already done before)

				BTret=BTRegisterDeviceInfo(&bt_di[i]);

				if(BTret != BTERR_SUCCESS){
					MessageBox(NULL, TEXT("BT register printer info error!"), TEXT("ERROR"), MB_OK | MB_ICONWARNING | MB_SETFOREGROUND);
					BTDeInitialize();		// deinitialize bt device (power off)
					return(-4);
				}
				PrinterFound=TRUE;
				MessageBox(NULL, TEXT("Printer Found"), TEXT("OK"),MB_OK);
				ii=i;
				break;
			}
		}
	}
	else
	{
			MessageBox(NULL, TEXT("BT Get device info error!"), TEXT("ERROR"), MB_OK | MB_ICONWARNING | MB_SETFOREGROUND);
			BTDeInitialize();		// deinitialize bt device (power off)
			return(-5);
	}

	if(PrinterFound == FALSE){
		MessageBox(NULL, TEXT("BT Printer not found!"), TEXT("ERROR"), MB_OK | MB_ICONWARNING | MB_SETFOREGROUND);
		BTDeInitialize();		// deinitialize bt device (power off)
		return(-7);
	}
	// set printer as default device
	// (can be skiped)

	BTret=BTSetDefaultDevice(&bt_di[ii], BTPORT_SERIAL);

	if(BTret != BTERR_SUCCESS){
		MessageBox(NULL, TEXT("BT Printer set default device error!"), TEXT("ERROR"), MB_OK | MB_ICONWARNING | MB_SETFOREGROUND);
		BTDeInitialize();		// deinitialize bt device (power off)
		return(-8);
	}

	SysSetLED(LED_RED, 0, 2, 2);

	// open serial communication
	hSerial=PortOpen(TEXT("COM6:"), CBR_9600, 8, NOPARITY, ONESTOPBIT, 3000, 3000);
	if(hSerial == INVALID_HANDLE_VALUE){
		MessageBox(NULL, TEXT("BT serial open error!"), TEXT("ERROR"), MB_OK | MB_ICONWARNING | MB_SETFOREGROUND);
		BTDeInitialize();		// deinitialize bt device (power off)
		return(-9);
	}
	// dummy write to etablish bt connection
	PortWrite(NULL, 0, hSerial);
	// try 50 times to wait for connection (max. 10 seconds)
	for(i=0; i<75; i++){
		GetCommModemStatus(hSerial, &dwCommStatus);
		if(dwCommStatus & (MS_RING_ON | MS_RLSD_ON)){
			//connected
			SysSetLED(LED_OFF, 0, 0, 0);
			SysSetLED(LED_GREEN, 32, 2, 2);		// led on for 2sec with 125ms on/off flashing
			break;
		}
		Sleep(200);
	}
	// write label to printer
	PortWrite(label1, strlen(label1), hSerial);
	MessageBox(NULL, TEXT("Port Write 1"), TEXT("OK"),MB_OK);

	PortClose(hSerial);

	MessageBox(NULL, TEXT("Power Off?"), TEXT("OK"),MB_OK);

// Comments : after finish print out operation, you should execute port close operation,
//	then you can turn off print power, then when you want to continue print out operation,
//  you should start from port open routine like as below.



	hSerial=PortOpen(TEXT("COM6:"), CBR_9600, 8, NOPARITY, ONESTOPBIT, 3000, 3000);
	if(hSerial == INVALID_HANDLE_VALUE){
		MessageBox(NULL, TEXT("BT serial open error!"), TEXT("ERROR"), MB_OK | MB_ICONWARNING | MB_SETFOREGROUND);
		BTDeInitialize();		// deinitialize bt device (power off)
		return(-9);
	}
	// dummy write to etablish bt connection
	PortWrite(NULL, 0, hSerial);
	// try 50 times to wait for connection (max. 10 seconds)
	for(i=0; i<75; i++){
		GetCommModemStatus(hSerial, &dwCommStatus);
		if(dwCommStatus & (MS_RING_ON | MS_RLSD_ON)){
			//connected
			SysSetLED(LED_OFF, 0, 0, 0);
			SysSetLED(LED_GREEN, 32, 2, 2);		// led on for 2sec with 125ms on/off flashing
			break;
		}
		Sleep(200);
	}
	// write label to printer
	PortWrite(label1, strlen(label1), hSerial);
	MessageBox(NULL, TEXT("Port Write 2"), TEXT("OK"),MB_OK);
	PortClose(hSerial);


	BTDeInitialize();		// deinitialize bt device (power off)
	SysSetLED(LED_OFF, 0, 0, 0);
	return 0;
}

void	WaitCurON(void)
{
	hCursor=GetCursor();
	SetCursor(LoadCursor(NULL, IDC_WAIT));
}

void	WaitCurOFF(void)
{
	SetCursor(hCursor);
}

//**********************************************************************
//**********************************************************************
// PORT IO FUNCTIONS
//**********************************************************************
//**********************************************************************


/***********************************************************************
  PortOpen (LPTSTR lpszPortName)
  return porthandle or INVALID_HANDLE_VALUE if error
***********************************************************************/
HANDLE PortOpen (LPTSTR lpszPortName, DWORD baudrate, BYTE Data, BYTE Parity, BYTE Stop, DWORD SendTime, DWORD RecvTime)
{
	DCB			PortDCB;
	HANDLE		hPort;

	// Open the port
	hPort = CreateFile (lpszPortName, GENERIC_READ | GENERIC_WRITE, 0, NULL, OPEN_EXISTING, 0, NULL);
	if(hPort != INVALID_HANDLE_VALUE){

		PortTimeouts(SendTime, RecvTime, hPort);		// set comtimeouts

		GetCommState (hPort, &PortDCB);

		PortDCB.BaudRate = baudrate;          // Current baud 
		PortDCB.fBinary = TRUE;               // Binary mode; no EOF check 
		if(Parity == NOPARITY)
			PortDCB.fParity = FALSE;          // Disable parity checking. 
		else
			PortDCB.fParity = TRUE;           // Enable parity checking. 
		PortDCB.fOutxDsrFlow = FALSE;         // No DSR output flow control 
		PortDCB.fDtrControl = DTR_CONTROL_ENABLE; 
		PortDCB.fRtsControl = RTS_CONTROL_ENABLE; 
		PortDCB.fOutxCtsFlow = FALSE;         // No CTS output flow control 
		PortDCB.fDsrSensitivity = FALSE;      // DSR sensitivity 
		PortDCB.ByteSize = Data;              // Number of bits/bytes, 4-8 
		PortDCB.Parity = Parity;              // 0-4=no,odd,even,mark,space 
		PortDCB.StopBits = Stop;              // 0,1,2 = 1, 1.5, 2 

		SetCommState (hPort, &PortDCB);
	}

	return hPort;
}

/***********************************************************************
  PortTimeouts
***********************************************************************/
void PortTimeouts (DWORD SendTime, DWORD RecvTime, HANDLE hPort)
{
	COMMTIMEOUTS	CommTimeouts;

	GetCommTimeouts (hPort, &CommTimeouts);
	CommTimeouts.ReadIntervalTimeout = 0;
	CommTimeouts.ReadTotalTimeoutMultiplier = 0;
	CommTimeouts.ReadTotalTimeoutConstant = RecvTime;
	CommTimeouts.WriteTotalTimeoutMultiplier = 0;
	CommTimeouts.WriteTotalTimeoutConstant = SendTime;
	SetCommTimeouts(hPort, &CommTimeouts);
}

/***********************************************************************
  PortWrite
***********************************************************************/
DWORD PortWrite (char *buffer, DWORD noBytes, HANDLE hPort)
{
	DWORD	dwNumBytesWritten;

	if(!WriteFile (hPort, buffer, noBytes, &dwNumBytesWritten, NULL)){
		return(-1);
	}
	return(dwNumBytesWritten);
}

/***********************************************************************
  PortClose (HANDLE hCommPort)
***********************************************************************/
void PortClose (HANDLE hPort)
{
	if(hPort != INVALID_HANDLE_VALUE){
		CloseHandle(hPort);
	}
}
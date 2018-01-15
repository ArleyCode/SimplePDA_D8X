// FlinkLibSampleVCDlg.cpp : implementation file
//

#include "stdafx.h"
#include "FlinkLibSampleVC.h"
#include "FlinkLibSampleVCDlg.h"
#include "FlinkLib.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

// CFlinkLibSampleVCDlg dialog

CFlinkLibSampleVCDlg::CFlinkLibSampleVCDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CFlinkLibSampleVCDlg::IDD, pParent)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CFlinkLibSampleVCDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_PROGRESS1, m_progress1);
	DDX_Control(pDX, IDC_PROGRESS2, m_progress2);
}

BEGIN_MESSAGE_MAP(CFlinkLibSampleVCDlg, CDialog)
#if defined(_DEVICE_RESOLUTION_AWARE) && !defined(WIN32_PLATFORM_WFSP)
	ON_WM_SIZE()
#endif
	//}}AFX_MSG_MAP
	ON_WM_TIMER()
	ON_BN_CLICKED(IDC_BUTTON4, &CFlinkLibSampleVCDlg::OnBnClickedButton4)
	ON_BN_CLICKED(IDC_BUTTON1, &CFlinkLibSampleVCDlg::OnBnClickedButton1)
	ON_BN_CLICKED(IDC_BUTTON2, &CFlinkLibSampleVCDlg::OnBnClickedButton2)
	ON_BN_CLICKED(IDC_BUTTON3, &CFlinkLibSampleVCDlg::OnBnClickedButton3)
	ON_BN_CLICKED(IDC_BUTTON5, &CFlinkLibSampleVCDlg::OnBnClickedButton5)
	ON_WM_CLOSE()
END_MESSAGE_MAP()


// CFlinkLibSampleVCDlg message handlers

BOOL CFlinkLibSampleVCDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon

	// TODO: Add extra initialization here
	
	return TRUE;  // return TRUE  unless you set the focus to a control
}

#if defined(_DEVICE_RESOLUTION_AWARE) && !defined(WIN32_PLATFORM_WFSP)
void CFlinkLibSampleVCDlg::OnSize(UINT /*nType*/, int /*cx*/, int /*cy*/)
{
	if (AfxIsDRAEnabled())
	{
		DRA::RelayoutDialog(
			AfxGetResourceHandle(), 
			this->m_hWnd, 
			DRA::GetDisplayMode() != DRA::Portrait ? 
			MAKEINTRESOURCE(IDD_FLINKLIBSAMPLEVC_DIALOG_WIDE) : 
			MAKEINTRESOURCE(IDD_FLINKLIBSAMPLEVC_DIALOG));
	}
}
#endif


void CFlinkLibSampleVCDlg::OnTimer(UINT_PTR nIDEvent)
{
	if(nIDEvent == 1)
	{
		FLK_STATUS flkstatus;

		// Stop timer.
		KillTimer(1);
		
		FLKReadStatus(m_port, &flkstatus);
		if(flkstatus.status == FLK_STATUS_ERROR)
		{	// Error occurred.
			MessageBox(_T("Command Error"));
			return;
		}
		if(flkstatus.status == FLK_STATUS_END)
		{	// Complete operation.
			MessageBox(_T("Command complete"));
			return;
		}
		// Update display.
		CWnd* hWnd = (CWnd*)GetDlgItem(IDC_FILENAME);
		hWnd->SetWindowText(flkstatus.FileName);
		if(flkstatus.file_size != 0)
		{
			m_progress1.SetPos(flkstatus.file_count *100/ flkstatus.file_size);
			m_progress2.SetPos(flkstatus.total_count * 100 / flkstatus.total_size);
		}
		
		// Restart timer.
		SetTimer(1, 100, NULL);
	}

	CDialog::OnTimer(nIDEvent);
}

void CFlinkLibSampleVCDlg::OnBnClickedButton4()
{
	char		ipadr[100];
	FLK_RSPRM	rsprm;
	
	// IT-3100
	//m_port = FLKOpen(_T("IrDA"), ipadr, &rsprm, FLK_MODE_HT, NULL, 0);
	 //IT-600 DT-X7
	m_port = FLKOpen(_T("USB"), ipadr, &rsprm, FLK_MODE_HT, NULL, 0);
	if((m_port == (HANDLE)FLK_NG) || (m_port == (HANDLE)FLK_PRM))
	{
		MessageBox(_T("FLKOpen Error"));
		return;
	}

	// Timer sets.
	SetTimer(1, 100, NULL);
}

void CFlinkLibSampleVCDlg::OnBnClickedButton1()
{	// Idle : The LMWin application command of PC is executed.
	DWORD ret;
	
	ret = FLKIdle(m_port, NULL);
	if(ret != FLK_OK)
	{
		MessageBox(_T("FLKIdle Error"));
		return;
	}
	// Timer sets.
	SetTimer(1, 100, NULL);
}

void CFlinkLibSampleVCDlg::OnBnClickedButton2()
{	// Send : The file that exists in the terminal is transmitted to PC. LMWin only has to start.
	DWORD ret;
		
	// All files under the temp folder on the terminal are transmitted to the C:\temp folder of PC.
	ret = FLKFileSend(m_port, FLK_TRANS_NORMAL, _T("\\temp\\*.*"), _T("C:\\temp\\"), FLK_PROTECT_VALID);
	if(ret != FLK_OK)
	{
		MessageBox(_T("FLKSendFile Error"));
		return;
	}
	// Timer sets.
	SetTimer(1, 100, NULL);
}

void CFlinkLibSampleVCDlg::OnBnClickedButton3()
{	// Receive : The file on PC is received to the terminal. LMWin only has to start.
	DWORD ret;
	
	// All files that exist in the C:\temp folder on PC are received to the temp folder on the terminal.
	ret = FLKFileRecv(m_port, FLK_TRANS_NORMAL, _T("C:\\temp\\*.*"), _T("\\temp\\"), FLK_PROTECT_VALID);
	if(ret != FLK_OK)
	{
		MessageBox(_T("FLKReceiveFile Error"));
		return;
	}
	// Timer sets.
	SetTimer(1, 100, NULL);
}

void CFlinkLibSampleVCDlg::OnBnClickedButton5()
{	// Disconnect
	FLKClose(m_port, FLK_CLOSE_NORMAL);
}

void CFlinkLibSampleVCDlg::OnClose()
{	// Disconnect Flink
	FLKClose(m_port, FLK_CLOSE_NORMAL);

	CDialog::OnClose();
}

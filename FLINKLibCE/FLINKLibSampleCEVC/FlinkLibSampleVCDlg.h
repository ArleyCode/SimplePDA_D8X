// FlinkLibSampleVCDlg.h : header file
//

#pragma once
#include "afxcmn.h"

// CFlinkLibSampleVCDlg dialog
class CFlinkLibSampleVCDlg : public CDialog
{
// Construction
public:
	CFlinkLibSampleVCDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	enum { IDD = IDD_FLINKLIBSAMPLEVC_DIALOG };


	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support

// Implementation
protected:
	HICON m_hIcon;

	// Generated message map functions
	virtual BOOL OnInitDialog();
#if defined(_DEVICE_RESOLUTION_AWARE) && !defined(WIN32_PLATFORM_WFSP)
	afx_msg void OnSize(UINT /*nType*/, int /*cx*/, int /*cy*/);
#endif
	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnTimer(UINT_PTR nIDEvent);
private:
	HANDLE m_port;
	CProgressCtrl m_progress1;
	CProgressCtrl m_progress2;
public:
	afx_msg void OnBnClickedButton4();
	afx_msg void OnBnClickedButton1();
	afx_msg void OnBnClickedButton2();
	afx_msg void OnBnClickedButton3();
	afx_msg void OnBnClickedButton5();
	afx_msg void OnClose();
};

package com.workshare.compareservices.client;
import javax.swing.JPanel;

import java.awt.GridBagLayout;
import java.awt.GridBagConstraints;

import javax.swing.JTextField;
import javax.swing.JButton;
import javax.swing.JFileChooser;
import javax.swing.JComboBox;
import javax.swing.JLabel;

import com.workshare.compareservices._5_0.comparewebservice.ResponseOptions;

public class CompareOutputPanel extends JPanel {

	private JTextField redlineTextField = null;
	private JTextField redlineMlTextField = null;
	private JTextField xmlSummaryTextField = null;
	private JButton redlineButton = null;
	private JButton redlineMlButton = null;
	private JButton xmlSummaryButton = null;
	private JLabel redlineLabel = null;
	private JLabel redlineMlLabel = null;
	private JLabel xmlSummaryLabel = null;
	private JFileChooser redlineFileChooser = null;  //  @jve:decl-index=0:visual-constraint="148,127"
	private JFileChooser redlineMlFileChooser = null;  //  @jve:decl-index=0:visual-constraint="148,127"
	private JFileChooser xmlSummaryFileChooser = null;  //  @jve:decl-index=0:visual-constraint="671,277"
	private JComboBox responseTypeChooser = null;
	private JLabel responseTypeLabel = null;
		
	/**
	 * This is the default constructor
	 */
	public CompareOutputPanel() {
		super();
		initialize();
	}

	/**
	 * This method initializes this
	 * 
	 * @return void
	 */
	private void initialize() {
		GridBagConstraints gridBagConstraints4 = new GridBagConstraints();
		gridBagConstraints4.gridx = 0;
		gridBagConstraints4.fill = java.awt.GridBagConstraints.HORIZONTAL;
		gridBagConstraints4.insets = new java.awt.Insets(3,3,3,3);
		gridBagConstraints4.gridy = 3;
		
		GridBagConstraints gridBagConstraints31 = new GridBagConstraints();
		gridBagConstraints31.gridx = 0;
		gridBagConstraints31.fill = java.awt.GridBagConstraints.HORIZONTAL;
		gridBagConstraints31.insets = new java.awt.Insets(3,3,3,3);
		gridBagConstraints31.gridy = 1;
		
		GridBagConstraints gridBagConstraints31Ml = new GridBagConstraints();
		gridBagConstraints31Ml.gridx = 0;
		gridBagConstraints31Ml.fill = java.awt.GridBagConstraints.HORIZONTAL;
		gridBagConstraints31Ml.insets = new java.awt.Insets(3,3,3,3);
		gridBagConstraints31Ml.gridy = 2;
		
		GridBagConstraints gridBagConstraints21 = new GridBagConstraints();
		gridBagConstraints21.gridx = 2;
		gridBagConstraints21.insets = new java.awt.Insets(3,3,3,3);
		gridBagConstraints21.gridy = 3;
		GridBagConstraints gridBagConstraints11 = new GridBagConstraints();
		gridBagConstraints11.gridx = 2;
		gridBagConstraints11.insets = new java.awt.Insets(3,3,3,3);
		gridBagConstraints11.gridy = 1;
		
		
		GridBagConstraints gridBagConstraints11Ml = new GridBagConstraints();
		gridBagConstraints11Ml.gridx = 2;
		gridBagConstraints11Ml.insets = new java.awt.Insets(3,3,3,3);
		gridBagConstraints11Ml.gridy = 2;
		
		GridBagConstraints gridBagConstraints3 = new GridBagConstraints();
		gridBagConstraints3.fill = java.awt.GridBagConstraints.HORIZONTAL;
		gridBagConstraints3.gridy = 3;
		gridBagConstraints3.weightx = 1.0;
		gridBagConstraints3.insets = new java.awt.Insets(3,3,3,3);
		gridBagConstraints3.gridx = 1;
		GridBagConstraints gridBagConstraints2 = new GridBagConstraints();
		gridBagConstraints2.fill = java.awt.GridBagConstraints.HORIZONTAL;
		gridBagConstraints2.gridy = 1;
		gridBagConstraints2.weightx = 1.0;
		gridBagConstraints2.insets = new java.awt.Insets(3,3,3,3);
		gridBagConstraints2.gridx = 1;
		
		GridBagConstraints gridBagConstraints2Ml = new GridBagConstraints();
		gridBagConstraints2Ml.fill = java.awt.GridBagConstraints.HORIZONTAL;
		gridBagConstraints2Ml.gridy = 2;
		gridBagConstraints2Ml.weightx = 1.0;
		gridBagConstraints2Ml.insets = new java.awt.Insets(3,3,3,3);
		gridBagConstraints2Ml.gridx = 1;
		
		GridBagConstraints gridBagConstraints51 = new GridBagConstraints();
		gridBagConstraints51.gridy = 0;
		gridBagConstraints51.fill = java.awt.GridBagConstraints.HORIZONTAL;
		gridBagConstraints51.insets = new java.awt.Insets(3, 3, 3, 3);
		gridBagConstraints51.gridx = 1;
		GridBagConstraints gridBagConstraints52 = new GridBagConstraints();
		gridBagConstraints52.fill = java.awt.GridBagConstraints.HORIZONTAL;
		gridBagConstraints52.gridy = 0;
		gridBagConstraints52.insets = new java.awt.Insets(3,3,3,3);
		gridBagConstraints52.gridx = 0;
		
		this.setLayout(new GridBagLayout());
		this.setSize(470, 84);
		this.setBorder(javax.swing.BorderFactory.createLineBorder(java.awt.SystemColor.controlShadow,1));
		
		this.add(getRedlineTextField(), gridBagConstraints2);
		this.add(getRedlineMlTextField(), gridBagConstraints2Ml);
		this.add(getXmlSummaryTextField(), gridBagConstraints3);
		
		this.add(getRedlineButton(), gridBagConstraints11);
		this.add(getRedlineMlButton(), gridBagConstraints11Ml);
		this.add(getXmlSummaryButton(), gridBagConstraints21);
		
		this.add(getRedlineLabel(), gridBagConstraints31);
		this.add(getRedlineMlLabel(), gridBagConstraints31Ml);
		this.add(getXmlSummaryLabel(), gridBagConstraints4);
		
		this.add(getResponseTypeChooser(), gridBagConstraints51);
		this.add(getResponseTypeLabel(), gridBagConstraints52);
	}

	/**
	 * This method initializes redlineTextField	
	 * 	
	 * @return javax.swing.JTextField	
	 */
	private JTextField getRedlineTextField() {
		if (redlineTextField == null) {			
			redlineTextField = new JTextField();
		}
		return redlineTextField;
	}
	
	/**
	 * This method initializes redlineMlTextField	
	 * 	
	 * @return javax.swing.JTextField	
	 */
	private JTextField getRedlineMlTextField() {
		if (redlineMlTextField == null) {			
			redlineMlTextField = new JTextField();
		}
		return redlineMlTextField;
	}

	/**
	 * This method initializes xmlSummaryTextField	
	 * 	
	 * @return javax.swing.JTextField	
	 */
	private JTextField getXmlSummaryTextField() {
		if (xmlSummaryTextField == null) {
			xmlSummaryTextField = new JTextField();
		}
		return xmlSummaryTextField;
	}
	
	/**
	 * This method initializes redlineButton	
	 * 	
	 * @return javax.swing.JButton	
	 */
	private JButton getRedlineButton() {
		if (redlineButton == null) {
			redlineButton = new JButton();
			redlineButton.setPreferredSize(new java.awt.Dimension(20,20));
			redlineButton.setText("...");
			redlineButton.addActionListener(new java.awt.event.ActionListener() {
				public void actionPerformed(java.awt.event.ActionEvent e) {
					if(getRedlineFileChooser().showSaveDialog(null) == JFileChooser.APPROVE_OPTION) {
						setRedlineFileName(getRedlineFileChooser().getSelectedFile().getAbsolutePath());						
					}			
				}
			});
		}
		return redlineButton;
	}
	
	/**
	 * This method initializes redlineMlButton	
	 * 	
	 * @return javax.swing.JButton	
	 */
	private JButton getRedlineMlButton() {
		if (redlineMlButton == null) {
			redlineMlButton = new JButton();
			redlineMlButton.setPreferredSize(new java.awt.Dimension(20,20));
			redlineMlButton.setText("...");
			redlineMlButton.addActionListener(new java.awt.event.ActionListener() {
				public void actionPerformed(java.awt.event.ActionEvent e) {
					if(getRedlineMlFileChooser().showSaveDialog(null) == JFileChooser.APPROVE_OPTION) {
						setRedlineMlFileName(getRedlineMlFileChooser().getSelectedFile().getAbsolutePath());						
					}			
				}
			});
		}
		return redlineMlButton;
	}

	/**
	 * This method initializes xmlSummaryButton	
	 * 	
	 * @return javax.swing.JButton	
	 */
	private JButton getXmlSummaryButton() {
		if (xmlSummaryButton == null) {
			xmlSummaryButton = new JButton();
			xmlSummaryButton.setPreferredSize(new java.awt.Dimension(20,20));
			xmlSummaryButton.setText("...");
			xmlSummaryButton.addActionListener(new java.awt.event.ActionListener() {
				public void actionPerformed(java.awt.event.ActionEvent e) {
					if(getXmlSummaryFileChooser().showSaveDialog(null) == JFileChooser.APPROVE_OPTION) {
						setXmlSummaryFileName(getXmlSummaryFileChooser().getSelectedFile().getAbsolutePath());						
					}	
				}
			});
		}
		return xmlSummaryButton;
	}

	/**
	 * This method initializes redlineCheckBox	
	 * 	
	 * @return javax.swing.JCheckBox	
	 */
	private JLabel getRedlineLabel() {
		if (redlineLabel == null) {
			redlineLabel = new JLabel("Redline:");
		}
		return redlineLabel;
	}

	/**
	 * This method initializes redlineMlCheckBox	
	 * 	
	 * @return javax.swing.JCheckBox	
	 */
	private JLabel getRedlineMlLabel() {
		if (redlineMlLabel == null) {
			redlineMlLabel = new JLabel("Redline Ml:");
		}
		return redlineMlLabel;
	}
	
	/**
	 * This method initializes xmlSummaryCheckBox	
	 * 	
	 * @return javax.swing.JCheckBox	
	 */
	private JLabel getXmlSummaryLabel() {
		if (xmlSummaryLabel == null) {
			xmlSummaryLabel = new JLabel("XML Summary:");
		}
		return xmlSummaryLabel;
	}
	
	public ResponseOptions getResponseFlags() throws Exception
	{
		return ResponseOptions.fromValue((String)getResponseTypeChooser().getSelectedItem());	
	}
	
	public String getRedlineFileName()
	{
		return getRedlineTextField().getText();
	}
	
	public void setRedlineFileName(String value)
	{
		getRedlineTextField().setText(value);
	}

	public String getRedlineMlFileName()
	{
		return getRedlineMlTextField().getText();
	}
	
	public void setRedlineMlFileName(String value)
	{
		getRedlineMlTextField().setText(value);
	}
	
	
	public String getXmlSummaryFileName()
	{
		return getXmlSummaryTextField().getText();
	}

	public void setXmlSummaryFileName(String value)
	{
		getXmlSummaryTextField().setText(value);
	}

	/**
	 * This method initializes redlineFileChooser	
	 * 	
	 * @return javax.swing.JFileChooser	
	 */
	private JFileChooser getRedlineFileChooser() {
		if (redlineFileChooser == null) {
			redlineFileChooser = new JFileChooser();
			redlineFileChooser.setSize(new java.awt.Dimension(472,314));
		}
		return redlineFileChooser;
	}

	
	/**
	 * This method initializes redlineFileChooser	
	 * 	
	 * @return javax.swing.JFileChooser	
	 */
	private JFileChooser getRedlineMlFileChooser() {
		if (redlineMlFileChooser == null) {
			redlineMlFileChooser = new JFileChooser();
			redlineMlFileChooser.setSize(new java.awt.Dimension(472,314));
		}
		return redlineMlFileChooser;
	}

	
	/**
	 * This method initializes xmlSummaryFileChooser	
	 * 	
	 * @return javax.swing.JFileChooser	
	 */
	private JFileChooser getXmlSummaryFileChooser() {
		if (xmlSummaryFileChooser == null) {
			xmlSummaryFileChooser = new JFileChooser();
		}
		return xmlSummaryFileChooser;
	}
	
	/**
	 * This method initializes resultChooser
	 * 
	 * @return javax.swing.JComboBox
	 */
	private JComboBox getResponseTypeChooser() {		
		if (responseTypeChooser == null) {
			ResponseOptions[] arrayRo=ResponseOptions.values() ;
			String[] results = new String[arrayRo.length] ;
			
			for (int i = 0; i < arrayRo.length; i++) {
				results[i]=arrayRo[i].value() ;
			}

			responseTypeChooser = new JComboBox(results);
			responseTypeChooser.addActionListener(new java.awt.event.ActionListener()
			{
				public void actionPerformed(java.awt.event.ActionEvent e)
				{
					JComboBox obj = (JComboBox)e.getSource();
					String item = (String)obj.getSelectedItem();
					
					boolean responseSummary = false;
					boolean responseMlRedline=false;
					boolean responseRedline = true;
					
					if (item.contains("Summary"))
					{
						responseSummary = true;
					} 
					
					if(item.contains("RedlinMl")){
						responseMlRedline=true ;
					}
					
					if (item == "Xml") {
						responseRedline = false;
						responseSummary = true;
						responseMlRedline=false ;
					}
					
					if (item == "RedlinMl"){
						responseRedline = false;
						responseSummary = false;
						responseMlRedline=true ;
					}
					
					

					getXmlSummaryTextField().setEnabled(responseSummary);
					getXmlSummaryButton().setEnabled(responseSummary);

					getRedlineButton().setEnabled(responseRedline);
					getRedlineTextField().setEnabled(responseRedline);
					
					getRedlineMlButton().setEnabled(responseMlRedline);
					getRedlineMlTextField().setEnabled(responseMlRedline);
					
					
					/* Change file extension */
					String newExtension = null;
					
					if(item.toLowerCase().contains("pdf"))
					{
						newExtension = "pdf";
					}else if (item.toLowerCase().contains("docx")) {
						newExtension = "docx";
					} else if (item.toLowerCase().contains("doc")) {
						newExtension = "doc";
					} else if(item.toLowerCase().contains("wdf")) {
						newExtension = "wdf";
					}
					else {
						newExtension = "rtf";
					}
					
					String path = getRedlineFileName();
					int index = path.lastIndexOf('.');
					if (index != -1) 
					{
						path = path.substring(0, index);
						path = path += "." + newExtension;
					    setRedlineFileName(path);
					}
				}
			});
		}
		return responseTypeChooser;
	}

	/**
	 *  This method initializes responseTypeLabel
	 * 
	 * @return
	 */
	private JLabel getResponseTypeLabel() {
		if (responseTypeLabel == null) {;
			responseTypeLabel = new JLabel("Response Type:");			
		}
		return responseTypeLabel;
	}

}  

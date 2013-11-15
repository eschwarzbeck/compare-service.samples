import javax.swing.JPanel;
import java.awt.GridBagLayout;
import java.awt.GridBagConstraints;
import javax.swing.JTextField;
import javax.swing.JButton;
import javax.swing.JCheckBox;
import javax.swing.JFileChooser;
import javax.swing.JComboBox;
import javax.swing.JLabel;
import com.workshare.compareservices._1_1.comparewebservice.*;

import com.workshare.compareservices._1_1.comparewebservice.CompareResponseFlags;

public class CompareOutputPanel extends JPanel {

	private JTextField redlineTextField = null;
	private JTextField xmlSummaryTextField = null;
	private JButton redlineButton = null;
	private JButton xmlSummaryButton = null;
	private JLabel redlineLabel = null;
	private JLabel xmlSummaryLabel = null;
	private JFileChooser redlineFileChooser = null;  //  @jve:decl-index=0:visual-constraint="148,127"
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
		gridBagConstraints4.gridy = 2;
		GridBagConstraints gridBagConstraints31 = new GridBagConstraints();
		gridBagConstraints31.gridx = 0;
		gridBagConstraints31.fill = java.awt.GridBagConstraints.HORIZONTAL;
		gridBagConstraints31.insets = new java.awt.Insets(3,3,3,3);
		gridBagConstraints31.gridy = 1;
		GridBagConstraints gridBagConstraints21 = new GridBagConstraints();
		gridBagConstraints21.gridx = 2;
		gridBagConstraints21.insets = new java.awt.Insets(3,3,3,3);
		gridBagConstraints21.gridy = 2;
		GridBagConstraints gridBagConstraints11 = new GridBagConstraints();
		gridBagConstraints11.gridx = 2;
		gridBagConstraints11.insets = new java.awt.Insets(3,3,3,3);
		gridBagConstraints11.gridy = 1;
		GridBagConstraints gridBagConstraints3 = new GridBagConstraints();
		gridBagConstraints3.fill = java.awt.GridBagConstraints.HORIZONTAL;
		gridBagConstraints3.gridy = 2;
		gridBagConstraints3.weightx = 1.0;
		gridBagConstraints3.insets = new java.awt.Insets(3,3,3,3);
		gridBagConstraints3.gridx = 1;
		GridBagConstraints gridBagConstraints2 = new GridBagConstraints();
		gridBagConstraints2.fill = java.awt.GridBagConstraints.HORIZONTAL;
		gridBagConstraints2.gridy = 1;
		gridBagConstraints2.weightx = 1.0;
		gridBagConstraints2.insets = new java.awt.Insets(3,3,3,3);
		gridBagConstraints2.gridx = 1;
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
		this.add(getXmlSummaryTextField(), gridBagConstraints3);
		this.add(getRedlineButton(), gridBagConstraints11);
		this.add(getXmlSummaryButton(), gridBagConstraints21);
		this.add(getRedlineLabel(), gridBagConstraints31);
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
	
	public CompareResponseFlags getResponseFlags() throws Exception
	{
		return CompareResponseFlags.fromString((String)getResponseTypeChooser().getSelectedItem());	
	}
	
	public String getRedlineFileName()
	{
		return getRedlineTextField().getText();
	}
	
	public void setRedlineFileName(String value)
	{
		getRedlineTextField().setText(value);
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
			String[] results = new String[] {
					CompareResponseFlags._Both, // (Rtf with Xml)
					CompareResponseFlags._Xml,
					CompareResponseFlags._Rtf,
					CompareResponseFlags._Pdf,
					CompareResponseFlags._DocX,
					CompareResponseFlags._PdfWithXml,
					CompareResponseFlags._DocXWithXml,
			};
			responseTypeChooser = new JComboBox(results);
			responseTypeChooser.addActionListener(new java.awt.event.ActionListener()
			{
				public void actionPerformed(java.awt.event.ActionEvent e)
				{
					JComboBox obj = (JComboBox)e.getSource();
					String item = (String)obj.getSelectedItem();
					
					boolean responseSummary = false;
					boolean responseRedline = true;
					
					if (item == "Both" || item.contains("Xml"))
					{
						responseSummary = true;
					} 
					
					if (item == "Xml") {
						responseRedline = false;
					}

					getXmlSummaryTextField().setEnabled(responseSummary);
					getXmlSummaryButton().setEnabled(responseSummary);

					getRedlineButton().setEnabled(responseRedline);
					getRedlineTextField().setEnabled(responseRedline);
					
					
					/* Change file extension */
					String newExtension = null;
					if( item  == CompareResponseFlags._Pdf || item == CompareResponseFlags._PdfWithXml)
					{
						newExtension = "pdf";
					} else if (item == CompareResponseFlags._DocX || item == CompareResponseFlags._DocXWithXml) {
						newExtension = "docx";
					} else {
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

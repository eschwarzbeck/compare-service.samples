import javax.swing.JPanel;
import java.awt.GridBagLayout;
import javax.swing.JLabel;
import java.awt.GridBagConstraints;
import javax.swing.JTextField;
import javax.swing.JButton;
import javax.swing.JCheckBox;
import javax.swing.JFileChooser;

public class CompareInputPanel extends JPanel {

	private JLabel originalFileNameLabel = null;
	private JTextField originalFileNameTextField = null;
	private JButton originalFileNameButton = null;
	private JLabel modifiedFileNameLabel = null;
	private JTextField modifiedFileNameTextField = null;
	private JButton modifiedFileNameButton = null;
	private JCheckBox renderingSetCheckBox = null;
	private JTextField renderingSetTextField = null;
	private JButton renderingSetButton = null;
	private JFileChooser originalFileChooser = null;  //  @jve:decl-index=0:visual-constraint="552,78"
	private JFileChooser modifiedFileChooser = null;  //  @jve:decl-index=0:visual-constraint="24,307"
	private JFileChooser renderingSetFileChooser = null;  //  @jve:decl-index=0:visual-constraint="437,634"

	/**
	 * This is the default constructor
	 */
	public CompareInputPanel() {
		super();
		initialize();
	}
	
	public String getOriginalFileName()
	{
		return getOriginalFileNameTextField().getText();
	}
	
	public void setOriginalFileName(String value)
	{
		getOriginalFileNameTextField().setText(value);
	}
	
	public String getModifiedFileName()
	{
		return getModifiedFileNameTextField().getText();
	}
	
	public void setModifiedFileName(String value)
	{
		getModifiedFileNameTextField().setText(value);
	}
	
	public String getRenderingSetFileName()
	{
		if(getRenderingSetCheckBox().isSelected())
			return getRenderingSetTextField().getText();
		return null;
	}
	
	public void setRenderingSetFileName(String value)
	{
		getRenderingSetTextField().setText(value);
	}

	/**
	 * This method initializes this
	 * 
	 * @return void
	 */
	private void initialize() {
		GridBagConstraints gridBagConstraints8 = new GridBagConstraints();
		gridBagConstraints8.gridx = 2;
		gridBagConstraints8.insets = new java.awt.Insets(3,3,3,3);
		gridBagConstraints8.gridy = 2;
		GridBagConstraints gridBagConstraints7 = new GridBagConstraints();
		gridBagConstraints7.fill = java.awt.GridBagConstraints.HORIZONTAL;
		gridBagConstraints7.gridy = 2;
		gridBagConstraints7.weightx = 1.0;
		gridBagConstraints7.insets = new java.awt.Insets(3,3,3,3);
		gridBagConstraints7.gridx = 1;
		GridBagConstraints gridBagConstraints6 = new GridBagConstraints();
		gridBagConstraints6.gridx = 0;
		gridBagConstraints6.insets = new java.awt.Insets(3,3,3,3);
		gridBagConstraints6.anchor = java.awt.GridBagConstraints.WEST;
		gridBagConstraints6.fill = java.awt.GridBagConstraints.HORIZONTAL;
		gridBagConstraints6.gridy = 2;
		GridBagConstraints gridBagConstraints5 = new GridBagConstraints();
		gridBagConstraints5.gridx = 2;
		gridBagConstraints5.insets = new java.awt.Insets(3,3,3,3);
		gridBagConstraints5.gridy = 1;
		GridBagConstraints gridBagConstraints4 = new GridBagConstraints();
		gridBagConstraints4.fill = java.awt.GridBagConstraints.HORIZONTAL;
		gridBagConstraints4.gridy = 1;
		gridBagConstraints4.weightx = 1.0;
		gridBagConstraints4.insets = new java.awt.Insets(3,3,3,3);
		gridBagConstraints4.gridx = 1;
		GridBagConstraints gridBagConstraints3 = new GridBagConstraints();
		gridBagConstraints3.gridx = 0;
		gridBagConstraints3.insets = new java.awt.Insets(3,3,3,3);
		gridBagConstraints3.fill = java.awt.GridBagConstraints.HORIZONTAL;
		gridBagConstraints3.gridy = 1;
		modifiedFileNameLabel = new JLabel();
		modifiedFileNameLabel.setText("Modified Filename:");
		GridBagConstraints gridBagConstraints2 = new GridBagConstraints();
		gridBagConstraints2.gridx = 2;
		gridBagConstraints2.insets = new java.awt.Insets(3,3,3,3);
		gridBagConstraints2.gridy = 0;
		GridBagConstraints gridBagConstraints1 = new GridBagConstraints();
		gridBagConstraints1.fill = java.awt.GridBagConstraints.HORIZONTAL;
		gridBagConstraints1.gridy = 0;
		gridBagConstraints1.weightx = 1.0;
		gridBagConstraints1.insets = new java.awt.Insets(3,3,3,3);
		gridBagConstraints1.gridx = 1;
		GridBagConstraints gridBagConstraints = new GridBagConstraints();
		gridBagConstraints.gridx = 0;
		gridBagConstraints.insets = new java.awt.Insets(3,3,3,3);
		gridBagConstraints.fill = java.awt.GridBagConstraints.HORIZONTAL;
		gridBagConstraints.gridy = 0;
		originalFileNameLabel = new JLabel();
		originalFileNameLabel.setText("Original Filename:");
		this.setLayout(new GridBagLayout());
		this.setSize(300, 96);
		this.setBorder(javax.swing.BorderFactory.createLineBorder(java.awt.SystemColor.controlShadow,1));
		this.add(originalFileNameLabel, gridBagConstraints);
		this.add(getOriginalFileNameTextField(), gridBagConstraints1);
		this.add(getOriginalFileNameButton(), gridBagConstraints2);
		this.add(modifiedFileNameLabel, gridBagConstraints3);
		this.add(getModifiedFileNameTextField(), gridBagConstraints4);
		this.add(getModifiedFileNameButton(), gridBagConstraints5);
		this.add(getRenderingSetCheckBox(), gridBagConstraints6);
		this.add(getRenderingSetTextField(), gridBagConstraints7);
		this.add(getRenderingSetButton(), gridBagConstraints8);
	}

	/**
	 * This method initializes originalFileNameTextField	
	 * 	
	 * @return javax.swing.JTextField	
	 */
	private JTextField getOriginalFileNameTextField() {
		if (originalFileNameTextField == null) {
			originalFileNameTextField = new JTextField();
		}
		return originalFileNameTextField;
	}

	/**
	 * This method initializes originalFileNameButton	
	 * 	
	 * @return javax.swing.JButton	
	 */
	private JButton getOriginalFileNameButton() {
		if (originalFileNameButton == null) {
			originalFileNameButton = new JButton();
			originalFileNameButton.setText("...");
			originalFileNameButton.setPreferredSize(new java.awt.Dimension(20,20));
			originalFileNameButton.addActionListener(new java.awt.event.ActionListener() {
				public void actionPerformed(java.awt.event.ActionEvent e) {
					if(getOriginalFileChooser().showOpenDialog(null) == JFileChooser.APPROVE_OPTION) {
						setOriginalFileName(getOriginalFileChooser().getSelectedFile().getAbsolutePath());
					}
				}
			});
		}
		return originalFileNameButton;
	}

	/**
	 * This method initializes modifiedFileNameTextField	
	 * 	
	 * @return javax.swing.JTextField	
	 */
	private JTextField getModifiedFileNameTextField() {
		if (modifiedFileNameTextField == null) {
			modifiedFileNameTextField = new JTextField();
		}
		return modifiedFileNameTextField;
	}

	/**
	 * This method initializes modifiedFileNameButton	
	 * 	
	 * @return javax.swing.JButton	
	 */
	private JButton getModifiedFileNameButton() {
		if (modifiedFileNameButton == null) {
			modifiedFileNameButton = new JButton();
			modifiedFileNameButton.setPreferredSize(new java.awt.Dimension(20,20));
			modifiedFileNameButton.setText("...");
			modifiedFileNameButton.addActionListener(new java.awt.event.ActionListener() {
				public void actionPerformed(java.awt.event.ActionEvent e) {
					if(getModifiedFileChooser().showOpenDialog(null) == JFileChooser.APPROVE_OPTION) {
						setModifiedFileName(getModifiedFileChooser().getSelectedFile().getAbsolutePath());
					}
				}
			});
		}
		return modifiedFileNameButton;
	}

	/**
	 * This method initializes renderingSetCheckBox	
	 * 	
	 * @return javax.swing.JCheckBox	
	 */
	private JCheckBox getRenderingSetCheckBox() {
		if (renderingSetCheckBox == null) {
			renderingSetCheckBox = new JCheckBox();
			renderingSetCheckBox.setText("Rendering Set:");
			renderingSetCheckBox.setSelected(true);			
			renderingSetCheckBox.addChangeListener(new javax.swing.event.ChangeListener() {
				public void stateChanged(javax.swing.event.ChangeEvent e) {
					getRenderingSetButton().setEnabled(getRenderingSetCheckBox().isSelected());
					getRenderingSetTextField().setEnabled(getRenderingSetCheckBox().isSelected());
				}
			});
		}
		return renderingSetCheckBox;
	}

	/**
	 * This method initializes renderingSetTextField	
	 * 	
	 * @return javax.swing.JTextField	
	 */
	private JTextField getRenderingSetTextField() {
		if (renderingSetTextField == null) {
			renderingSetTextField = new JTextField();
		}
		return renderingSetTextField;
	}

	/**
	 * This method initializes renderingSetButton	
	 * 	
	 * @return javax.swing.JButton	
	 */
	private JButton getRenderingSetButton() {
		if (renderingSetButton == null) {
			renderingSetButton = new JButton();
			renderingSetButton.setPreferredSize(new java.awt.Dimension(20,20));
			renderingSetButton.setText("...");
			renderingSetButton.addActionListener(new java.awt.event.ActionListener() {
				public void actionPerformed(java.awt.event.ActionEvent e) {
					if(getRenderingSetFileChooser().showOpenDialog(null) == JFileChooser.APPROVE_OPTION) {
						setRenderingSetFileName(getRenderingSetFileChooser().getSelectedFile().getAbsolutePath());
					}
				}
			});
		}
		return renderingSetButton;
	}

	/**
	 * This method initializes originalFileChooser	
	 * 	
	 * @return javax.swing.JFileChooser	
	 */
	private JFileChooser getOriginalFileChooser() {
		if (originalFileChooser == null) {
			originalFileChooser = new JFileChooser();
		}
		return originalFileChooser;
	}

	/**
	 * This method initializes modifiedFileChooser	
	 * 	
	 * @return javax.swing.JFileChooser	
	 */
	private JFileChooser getModifiedFileChooser() {
		if (modifiedFileChooser == null) {
			modifiedFileChooser = new JFileChooser();
		}
		return modifiedFileChooser;
	}

	/**
	 * This method initializes renderingSetFileChooser	
	 * 	
	 * @return javax.swing.JFileChooser	
	 */
	private JFileChooser getRenderingSetFileChooser() {
		if (renderingSetFileChooser == null) {
			renderingSetFileChooser = new JFileChooser();
		}
		return renderingSetFileChooser;
	}

}  //  @jve:decl-index=0:visual-constraint="10,10"

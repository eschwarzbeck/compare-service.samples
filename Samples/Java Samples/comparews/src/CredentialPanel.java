import javax.swing.JPanel;
import java.awt.GridBagLayout;
import javax.swing.JLabel;
import java.awt.GridBagConstraints;
import javax.swing.JTextField;
import javax.swing.JPasswordField;

public class CredentialPanel extends JPanel {

	private JLabel endpointLabel = null;
	private JTextField endpointTextField = null;
	private JLabel usernameLabel = null;
	private JTextField usernameTextField = null;
	private JLabel passwordLabel = null;
	private JPasswordField passwordField = null;
	private JTextField domainTextField = null;
	private JLabel domainLabel = null;
	/**
	 * This is the default constructor
	 */
	public CredentialPanel() {
		super();
		initialize();
	}
	
	public String getendpoint()
	{
		return this.getendpointTextField().getText();
	}
	
	public void setendpoint(String value)
	{
		this.getendpointTextField().setText(value);
	}
	
	public String getUsername()
	{
		return this.getUsernameTextField().getText();
	}
	
	public void setUsername(String value)
	{
		this.getUsernameTextField().setText(value);
	}
	
	public String getPassword()
	{
		return this.getPasswordField().getText();
	}
	
	public void setPassword(String value)
	{
		this.getPasswordField().setText(value);
	}
	
	public String getDomain()
	{
		return this.getDomainTextField().getText();
	}
	
	public void setDomain(String value)
	{
		this.getDomainTextField().setText(value);
	}

	/**
	 * This method initializes this
	 * 
	 * @return void
	 */
	private void initialize() {
		GridBagConstraints gridBagConstraints6 = new GridBagConstraints();
		gridBagConstraints6.gridx = 0;
		gridBagConstraints6.fill = java.awt.GridBagConstraints.HORIZONTAL;
		gridBagConstraints6.insets = new java.awt.Insets(3,3,3,3);
		gridBagConstraints6.gridy = 3;
		endpointLabel = new JLabel();
		endpointLabel.setText("WCS Endpoint:");

		GridBagConstraints gridBagConstraints7 = new GridBagConstraints();
		gridBagConstraints7.fill = java.awt.GridBagConstraints.HORIZONTAL;
		gridBagConstraints7.gridy = 3;
		gridBagConstraints7.weightx = 1.0;
		gridBagConstraints7.insets = new java.awt.Insets(3,3,3,3);
		gridBagConstraints7.gridx = 1;

		GridBagConstraints gridBagConstraints5 = new GridBagConstraints();
		gridBagConstraints5.gridx = 0;
		gridBagConstraints5.fill = java.awt.GridBagConstraints.HORIZONTAL;
		gridBagConstraints5.insets = new java.awt.Insets(3,3,3,3);
		gridBagConstraints5.gridy = 2;
		domainLabel = new JLabel();
		domainLabel.setText("Windows Domain:");

		GridBagConstraints gridBagConstraints4 = new GridBagConstraints();
		gridBagConstraints4.fill = java.awt.GridBagConstraints.HORIZONTAL;
		gridBagConstraints4.gridy = 2;
		gridBagConstraints4.weightx = 1.0;
		gridBagConstraints4.insets = new java.awt.Insets(3,3,3,3);
		gridBagConstraints4.gridx = 1;

		GridBagConstraints gridBagConstraints3 = new GridBagConstraints();
		gridBagConstraints3.fill = java.awt.GridBagConstraints.HORIZONTAL;
		gridBagConstraints3.gridy = 1;
		gridBagConstraints3.weightx = 1.0;
		gridBagConstraints3.insets = new java.awt.Insets(3,3,3,3);
		gridBagConstraints3.gridx = 1;
		GridBagConstraints gridBagConstraints2 = new GridBagConstraints();
		gridBagConstraints2.gridx = 0;
		gridBagConstraints2.insets = new java.awt.Insets(3,3,3,3);
		gridBagConstraints2.fill = java.awt.GridBagConstraints.HORIZONTAL;
		gridBagConstraints2.gridy = 1;
		passwordLabel = new JLabel();
		passwordLabel.setText("Windows Password:");

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
		usernameLabel = new JLabel();
		usernameLabel.setText("Windows Username:");

		this.setLayout(new GridBagLayout());
		this.setSize(500, 300);
		this.setBorder(javax.swing.BorderFactory.createLineBorder(java.awt.SystemColor.controlShadow,1));
		this.add(usernameLabel, gridBagConstraints);
		this.add(getUsernameTextField(), gridBagConstraints1);
		this.add(passwordLabel, gridBagConstraints2);
		this.add(getPasswordField(), gridBagConstraints3);
		this.add(getDomainTextField(), gridBagConstraints4);
		this.add(domainLabel, gridBagConstraints5);
		this.add(endpointLabel, gridBagConstraints6);
		this.add(getendpointTextField(), gridBagConstraints7);
	}

	/**
	 * This method initializes endpointTextField	
	 * 	
	 * @return javax.swing.JTextField	
	 */
	private JTextField getendpointTextField() {
		if (endpointTextField == null) {
			endpointTextField = new JTextField();
		}
		return endpointTextField;
	}

	/**
	 * This method initializes usernameTextField	
	 * 	
	 * @return javax.swing.JTextField	
	 */
	private JTextField getUsernameTextField() {
		if (usernameTextField == null) {
			usernameTextField = new JTextField();
		}
		return usernameTextField;
	}

	/**
	 * This method initializes passwordField	
	 * 	
	 * @return javax.swing.JPasswordField	
	 */
	private JPasswordField getPasswordField() {
		if (passwordField == null) {
			passwordField = new JPasswordField();
		}
		return passwordField;
	}

	/**
	 * This method initializes domainTextField	
	 * 	
	 * @return javax.swing.JTextField	
	 */
	private JTextField getDomainTextField() {
		if (domainTextField == null) {
			domainTextField = new JTextField();
		}
		return domainTextField;
	}

}

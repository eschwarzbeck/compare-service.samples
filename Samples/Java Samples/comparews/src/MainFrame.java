import java.awt.BorderLayout;

import javax.security.auth.callback.TextInputCallback;
import javax.swing.JPanel;
import javax.swing.JOptionPane;
import javax.swing.JFrame;
import javax.swing.JInternalFrame;
import java.awt.GridBagLayout;
import java.awt.GridBagConstraints;
import java.awt.PageAttributes.OriginType;
import java.io.*;

import javax.xml.rpc.holders.StringHolder;
import javax.swing.JButton;
import javax.xml.rpc.holders.ByteArrayHolder;

import com.sun.mail.util.LineInputStream;

import sun.security.util.ByteArrayTagOrder;

import com.workshare.compareservices._1_1.comparewebservice.*;
import org.apache.axis.message.*;


public class MainFrame extends JFrame {

	private JPanel clientPanel = null;
	private CompareInputPanel compareInputPanel = null;
	private CompareOutputPanel compareOutputPanel = null;
	private CredentialPanel credentialPanel = null;
	private JPanel buttonPanel = null;
	private JButton compareButton = null;
	private JButton closeButton = null;
	/**
	 * This is the default constructor
	 */
	public MainFrame() {
		super();
		initialize();
	}

	/**
	 * This method initializes this
	 * 
	 * @return void
	 */
	private void initialize()
	{
		this.setSize(600, 420);
		this.setContentPane(getClientPanel());
		this.setDefaultCloseOperation(javax.swing.JFrame.EXIT_ON_CLOSE);
		this.setResizable(true);
		this.setTitle("Workshare DIS Compare Example");
		this.addWindowListener(new java.awt.event.WindowAdapter()
		{   
			public void windowOpened(java.awt.event.WindowEvent e)
			{
				try
				{
					getCompareInputPanel().setOriginalFileName(findFile("original.doc"));
					getCompareInputPanel().setModifiedFileName(findFile("modified.doc"));
					getCompareInputPanel().setRenderingSetFileName(findFile("standard.set"));
					getCompareOutputPanel().setRedlineFileName(getAbsolutePath("redline.rtf"));
					getCompareOutputPanel().setXmlSummaryFileName(getAbsolutePath("redline.xml"));
					getCredentialPanel().setUsername(System.getenv("USERNAME"));
					getCredentialPanel().setDomain(System.getenv("USERDOMAIN"));

					String sHostFile = findFile("host.txt");
					String sHostUri = getStringFromFile(new File(sHostFile));

					getCredentialPanel().setendpoint(sHostUri);
				}
				catch(Exception ex)
				{
					JOptionPane.showMessageDialog(null, "Error:" + ex.toString());
				}
			}		
		});
	}
	
	private String getAbsolutePath(String filename)
	{
		File f = new File(filename);
		return f.getAbsolutePath();
	}
	
	private String findFile(String filename)
	{
		File f = new File("data\\" + filename);
		if(f.exists())
			return f.getAbsolutePath();				
		
		return "";		
	}

	/**
	 * This method initializes clientPanel	
	 * 	
	 * @return javax.swing.JPanel	
	 */
	private JPanel getClientPanel()
	{
		if (clientPanel == null)
		{
			GridBagConstraints gridBagConstraints6 = new GridBagConstraints();
			gridBagConstraints6.gridx = 3;
			gridBagConstraints6.fill = java.awt.GridBagConstraints.HORIZONTAL;
			gridBagConstraints6.anchor = java.awt.GridBagConstraints.WEST;
			gridBagConstraints6.gridy = 3;
			GridBagConstraints gridBagConstraints5 = new GridBagConstraints();
			gridBagConstraints5.gridx = 3;
			gridBagConstraints5.insets = new java.awt.Insets(7,7,7,7);
			gridBagConstraints5.ipadx = 7;
			gridBagConstraints5.ipady = 7;
			gridBagConstraints5.fill = java.awt.GridBagConstraints.HORIZONTAL;
			gridBagConstraints5.gridy = 2;
			GridBagConstraints gridBagConstraints1 = new GridBagConstraints();
			gridBagConstraints1.gridx = 3;
			gridBagConstraints1.fill = java.awt.GridBagConstraints.HORIZONTAL;
			gridBagConstraints1.insets = new java.awt.Insets(7,7,7,7);
			gridBagConstraints1.ipadx = 7;
			gridBagConstraints1.ipady = 7;
			gridBagConstraints1.gridy = 1;
			GridBagConstraints gridBagConstraints = new GridBagConstraints();
			gridBagConstraints.gridx = 3;
			gridBagConstraints.weightx = 9.0D;
			gridBagConstraints.insets = new java.awt.Insets(7,7,7,7);
			gridBagConstraints.fill = java.awt.GridBagConstraints.HORIZONTAL;
			gridBagConstraints.ipadx = 7;
			gridBagConstraints.ipady = 7;
			gridBagConstraints.gridy = 0;
			clientPanel = new JPanel();
			clientPanel.setLayout(new GridBagLayout());
			clientPanel.add(getCompareInputPanel(), gridBagConstraints);
			clientPanel.add(getCompareOutputPanel(), gridBagConstraints1);
			clientPanel.add(getCredentialPanel(), gridBagConstraints5);
			clientPanel.add(getButtonPanel(), gridBagConstraints6);
		}
		return clientPanel;
	}

	/**
	 * This method initializes compareInputPanel	
	 * 	
	 * @return CompareInputPanel	
	 */
	private CompareInputPanel getCompareInputPanel()
	{
		if (compareInputPanel == null)
		{
			compareInputPanel = new CompareInputPanel();
		}
		return compareInputPanel;
	}

	/**
	 * This method initializes compareOutputPanel	
	 * 	
	 * @return CompareOutputPanel	
	 */
	private CompareOutputPanel getCompareOutputPanel()
	{
		if (compareOutputPanel == null)
		{
			compareOutputPanel = new CompareOutputPanel();
		}
		return compareOutputPanel;
	}

	/**
	 * This method initializes credentialPanel	
	 * 	
	 * @return CredentialPanel	
	 */
	private CredentialPanel getCredentialPanel()
	{
		if (credentialPanel == null)
		{
			credentialPanel = new CredentialPanel();
		}
		return credentialPanel;
	}

	/**
	 * This method initializes buttonPanel	
	 * 	
	 * @return javax.swing.JPanel	
	 */
	private JPanel getButtonPanel()
	{
		if (buttonPanel == null)
		{			
			GridBagConstraints gridBagConstraints3 = new GridBagConstraints();
			gridBagConstraints3.insets = new java.awt.Insets(5,3,5,149);
			gridBagConstraints3.gridy = 0;
			gridBagConstraints3.anchor = java.awt.GridBagConstraints.EAST;
			gridBagConstraints3.gridx = 1;
			GridBagConstraints gridBagConstraints2 = new GridBagConstraints();
			gridBagConstraints2.insets = new java.awt.Insets(5,149,5,2);
			gridBagConstraints2.gridy = 0;
			gridBagConstraints2.anchor = java.awt.GridBagConstraints.EAST;
			gridBagConstraints2.weightx = 0.0D;
			gridBagConstraints2.gridx = 0;
			buttonPanel = new JPanel();
			buttonPanel.setLayout(new GridBagLayout());
			buttonPanel.add(getCompareButton(), gridBagConstraints2);
			buttonPanel.add(getCloseButton(), gridBagConstraints3);			
		}
		return buttonPanel;
	}

	/**
	 * This method initializes compareButton	
	 * 	
	 * @return javax.swing.JButton	
	 */
	private JButton getCompareButton()
	{
		if (compareButton == null)
		{
			compareButton = new JButton();
			compareButton.addActionListener(new java.awt.event.ActionListener()
			{
				public void actionPerformed(java.awt.event.ActionEvent e)
				{
					try
					{						
						CompareResponseFlags flags = getCompareOutputPanel().getResponseFlags();
						System.out.println("" + flags);

						CompareWebServiceSoapStub client = new CompareWebServiceSoapStub();
						client._setProperty("javax.xml.rpc.service.endpoint.address", getCredentialPanel().getendpoint());
						
						byte[] original = getBytesFromFile(getCompareInputPanel().getOriginalFileName());
						byte[] modified = getBytesFromFile(getCompareInputPanel().getModifiedFileName());
						ByteArrayHolder redline = new ByteArrayHolder();
						StringHolder xml = new StringHolder();

						String compareOptions = "";
						if (getCompareInputPanel().getRenderingSetFileName() != null)
						{
							compareOptions = getStringFromFile(new File(getCompareInputPanel().getRenderingSetFileName()));
						}
						else
						{
							compareOptions = "";
						}
						
						client.setUsername(getCredentialPanel().getDomain() + "\\" + getCredentialPanel().getUsername());
						client.setPassword(getCredentialPanel().getPassword());

						if (client.authenticate("DIS", "secret", "secret"))
						{
							SOAPHeaderElement[] headers = client.getResponseHeaders();
							int numberOfSoapHeaders = headers.length;

							for (int indexOfSoapHeader = 0; indexOfSoapHeader != numberOfSoapHeaders; indexOfSoapHeader++)
							{
								SOAPHeaderElement header = headers[indexOfSoapHeader];
								if (header.getLocalName().equals("SoapToken"))
									client.setHeader(header);
							}

							CompareResult result = client.execute2(original, modified, flags, compareOptions);

							if (flags != CompareResponseFlags.Xml)
							{
								saveBufferToFile(getCompareOutputPanel().getRedlineFileName(), result.getRedline());
							}

							if (flags == CompareResponseFlags.Both || flags.toString().contains("Xml"))
							{
								saveBufferToFile(getCompareOutputPanel().getXmlSummaryFileName(), (result.getSummary().get_any()[0]).toString().getBytes());
							}

							JOptionPane.showMessageDialog(null, "Done!");
						}
						else
						{
							JOptionPane.showMessageDialog(null, "Error Authenticating");
						}
					}
					catch(Exception ex)
					{
						JOptionPane.showMessageDialog(null, "Error:" + ex.toString());
					}
				}
			});
			compareButton.setText("Compare");			
		}
		return compareButton;
	}
	
	public static void saveBufferToFile(String filename, byte[] buffer) throws IOException
	{		 
		OutputStream w = new FileOutputStream(filename);
		try
		{
			w.write(buffer); 
		}
		finally
		{
			w.close();
		}              
	}

	   public static String getStringFromFile(File f)  throws IOException
	   {
       String result = null;
       
       long len = f.length();

       if (len <= 0L)
       {
           if (f.exists())
           {
               result = "";
           }
       }
       else if (len > Integer.MAX_VALUE)
       {
           throw new IOException("File too large!");
       }
       else
       {
               int length = (int)len;       
               FileInputStream fis = new FileInputStream(f);
               BufferedReader in = new BufferedReader(new InputStreamReader(fis));       
               char[] buf = new char[length];       
               in.read(buf, 0, length);
               fis.close();       
               result = String.valueOf(buf);
       }
       
       return result;
   }
	
	public byte[] getBytesFromFile(String file) throws IOException {
          InputStream is = new FileInputStream(file);
      
          File f = new File(file);      
          
          // Get the size of the file
          long length = f.length();
          // You cannot create an array using a long type.
          // It needs to be an int type.
          // Before converting to an int type, check
          // to ensure that file is not larger than Integer.MAX_VALUE.
          if (length > Integer.MAX_VALUE) {
              // File is too large
          }
      
          // Create the byte array to hold the data
          byte[] bytes = new byte[(int)length];
          System.out.println(bytes.length);
          // Read in the bytes
          int offset = 0;
          int numRead = 0;
          while (offset < bytes.length
                 && (numRead=is.read(bytes, offset, bytes.length-offset)) >= 0) {
              offset += numRead;
          }
      
          // Ensure all the bytes have been read in
          if (offset < bytes.length) {
              throw new IOException("Could not completely read file "+file);
          }
      
          // Close the input stream and return bytes
          is.close();
          return bytes;
      }
	/**
	 * This method initializes closeButton	
	 * 	
	 * @return javax.swing.JButton	
	 */
	private JButton getCloseButton() {
		if (closeButton == null) {
			closeButton = new JButton();
			closeButton.setText("Close");
			closeButton.addActionListener(new java.awt.event.ActionListener() {
				public void actionPerformed(java.awt.event.ActionEvent e) {
					System.exit(0);
				}
			});
		}
		return closeButton;
	}

}  //  @jve:decl-index=0:visual-constraint="10,10"

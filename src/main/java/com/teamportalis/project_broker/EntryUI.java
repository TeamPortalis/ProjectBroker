package com.teamportalis.project_broker;

import javax.servlet.annotation.WebServlet;

import com.vaadin.annotations.Theme;
import com.vaadin.annotations.VaadinServletConfiguration;
import com.vaadin.annotations.Widgetset;
import com.vaadin.server.Constants;
import com.vaadin.server.VaadinRequest;
import com.vaadin.server.VaadinServlet;
import com.vaadin.shared.ui.label.ContentMode;
import com.vaadin.ui.Button;
import com.vaadin.ui.FormLayout;
import com.vaadin.ui.Label;
import com.vaadin.ui.Layout;
import com.vaadin.ui.Panel;
import com.vaadin.ui.PasswordField;
import com.vaadin.ui.TextField;
import com.vaadin.ui.UI;
import com.vaadin.ui.VerticalLayout;
/**
 * This UI is the application entry point. A UI may either represent a browser window 
 * (or tab) or some part of a html page where a Vaadin application is embedded.
 * <p>
 * The UI is initialized using {@link #init(VaadinRequest)}. This method is intended to be 
 * overridden to add component to the user interface and initialize non-component functionality.
 */
@Theme("projectbrokertheme")
@Widgetset("com.teamportalis.project_broker.ProjectBrokerWidgetset")
public class EntryUI extends UI {
	private Layout layout = new VerticalLayout();
	private Button btnLogin = new Button("Login");
	private TextField txtUsername = new TextField("Username");
	private PasswordField pwdField = new PasswordField("Password");
	private Label lbErrorMessage = new Label();
	
    @Override
    protected void init(VaadinRequest vaadinRequest) {
    	txtUsername.setRequired(true);
    	pwdField.setRequired(true);
    	txtUsername.setRequiredError("Please enter a username!");
    	pwdField.setRequiredError("Please enter a password!");
    	lbErrorMessage.setContentMode(ContentMode.HTML);
    	lbErrorMessage.setVisible(false);
    	btnLogin.addClickListener(e ->
    	{
    		if(txtUsername.getValue().length() > 0 && pwdField.getValue().length() > 0)
    		{
    			lbErrorMessage.setValue("<p style='color:blue;'>Logged in as: "+txtUsername.getValue()+"</p>");
    			lbErrorMessage.setVisible(true);
    		}
    		else
    		{
    			lbErrorMessage.setValue("<p style='color:red;'>Please enter a username and password!</p>");
    			lbErrorMessage.setVisible(true);
    		}
    	});
    	
    	final Panel loginPanel = new Panel("Login ...");
    	layout.addComponent(loginPanel);
    	
    	final FormLayout loginForm = new FormLayout();
    	loginForm.setMargin(true);
    	loginForm.setStyleName("loginForm");
    	loginForm.addComponent(txtUsername);
    	loginForm.addComponent(pwdField);
    	loginForm.addComponent(btnLogin);
    	loginForm.addComponent(lbErrorMessage);
    	
    	loginPanel.setContent(loginForm);
    	setContent(layout);
    }

    @WebServlet(urlPatterns = "/*", name = "EntryUIServlet", asyncSupported = true)
    @VaadinServletConfiguration(ui = EntryUI.class, productionMode = false)
    public static class EntryUIServlet extends VaadinServlet {
    }
}

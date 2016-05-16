package com.teamportalis.project_broker.views;

import com.teamportalis.project_broker.presenters.LoginViewPresenter;
import com.vaadin.navigator.View;
import com.vaadin.navigator.ViewChangeListener;
import com.vaadin.server.VaadinSession;
import com.vaadin.shared.ui.label.ContentMode;
import com.vaadin.ui.*;

/**
 * Created by Moritz Brandstätter on 15.05.2016.
 */
public class LoginView extends CustomComponent implements View {

    private Button btnLogin = new Button("Login");
    private TextField txtUsername = new TextField("Username");
    private PasswordField pwdField = new PasswordField("Password");
    private Label lbErrorMessage = new Label();
    private LoginViewPresenter presenter;

    public LoginView(LoginViewPresenter presenter){
        this.presenter = presenter;
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
                presenter.onLogin(txtUsername.getValue(),pwdField.getValue());
            }
            else
            {
                Notification.show("Please enter a username and password!");
                lbErrorMessage.setValue("<p style='color:red;'>Please enter a username and password!</p>");
                lbErrorMessage.setVisible(true);
            }
        });

        final Panel loginPanel = new Panel("Login ...");

        final FormLayout loginForm = new FormLayout();
        loginForm.setMargin(true);
        loginForm.setStyleName("loginForm");
        loginForm.addComponent(txtUsername);
        loginForm.addComponent(pwdField);
        loginForm.addComponent(btnLogin);
        loginForm.addComponent(lbErrorMessage);

        loginPanel.setContent(loginForm);
        this.setCompositionRoot(loginPanel);
    }


    @Override
    public void enter(ViewChangeListener.ViewChangeEvent viewChangeEvent) {

    }
}

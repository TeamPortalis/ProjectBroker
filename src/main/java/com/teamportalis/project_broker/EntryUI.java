package com.teamportalis.project_broker;

import javax.servlet.annotation.WebServlet;

import com.teamportalis.project_broker.views.LoginView;
import com.vaadin.annotations.Theme;
import com.vaadin.annotations.VaadinServletConfiguration;
import com.vaadin.annotations.Widgetset;
import com.vaadin.navigator.Navigator;
import com.vaadin.server.Constants;
import com.vaadin.server.VaadinRequest;
import com.vaadin.server.VaadinServlet;
import com.vaadin.server.VaadinSession;
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

	private Navigator mainNavigator;
	private LoginView v;

	@Override
    protected void init(VaadinRequest vaadinRequest) {
		v = new LoginView();
		mainNavigator = new Navigator(this, this);
		mainNavigator.addView("", v);
		mainNavigator.navigateTo("");
    }

    @WebServlet(urlPatterns = "/*", name = "EntryUIServlet", asyncSupported = true)
    @VaadinServletConfiguration(ui = EntryUI.class, productionMode = false)
    public static class EntryUIServlet extends VaadinServlet {
    }
}

package com.teamportalis.project_broker;

import javax.servlet.annotation.WebServlet;

import com.teamportalis.project_broker.presenters.LoginViewPresenter;
import com.teamportalis.project_broker.presenters.ProjectViewPresenter;
import com.teamportalis.project_broker.views.LoginView;
import com.teamportalis.project_broker.views.ProjectView;
import com.vaadin.annotations.Theme;
import com.vaadin.annotations.VaadinServletConfiguration;
import com.vaadin.annotations.Widgetset;
import com.vaadin.navigator.Navigator;
import com.vaadin.server.Constants;
import com.vaadin.server.VaadinRequest;
import com.vaadin.server.VaadinServlet;
import com.vaadin.server.VaadinSession;
import com.vaadin.shared.ui.label.ContentMode;
import com.vaadin.ui.*;

import java.util.ArrayList;

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
	private LoginViewPresenter loginViewPresenter;
	private ProjectView p;
	private ProjectViewPresenter projectViewPresenter;

	@Override
    protected void init(VaadinRequest vaadinRequest) {
		loginViewPresenter = new LoginViewPresenter() {
			@Override
			public void onLogin(String user, String password) {
				//TODO implement real login-checking
				//Pseudo login for design-testing
				Notification.show("Welcome "+user+"!");
				mainNavigator.navigateTo("project");
			}
		};
		v = new LoginView(loginViewPresenter);
		projectViewPresenter = new ProjectViewPresenter()
		{
			@Override
			public ArrayList<Object> OnSearchButton() {
				return null; //TODO search mechanism
			}
		};
		p = new ProjectView(projectViewPresenter);
		mainNavigator = new Navigator(this, this);
		mainNavigator.addView("", v);
		mainNavigator.addView("project",p);
		mainNavigator.navigateTo("");
    }

    @WebServlet(urlPatterns = "/*", name = "EntryUIServlet", asyncSupported = true)
    @VaadinServletConfiguration(ui = EntryUI.class, productionMode = false)
    public static class EntryUIServlet extends VaadinServlet {
    }
}

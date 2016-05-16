package com.teamportalis.project_broker.views;

import com.teamportalis.project_broker.presenters.ProjectViewPresenter;
import com.vaadin.navigator.View;
import com.vaadin.navigator.ViewChangeListener;
import com.vaadin.ui.*;

/**
 * Created by Moritz Brandstätter on 15.05.2016.
 */
public class ProjectView extends CustomComponent implements View{
    private ProjectViewPresenter presenter;
    private Layout layout = new HorizontalLayout();
    private VerticalLayout menuBar = new VerticalLayout();
    private Panel menuBarouter = new Panel();
    private Button minimizeButton = new Button("Minimize");
    private Button projectsButton = new Button("Projects");
    private Button adminButton = new Button("Admin");
    private Button settingsButton = new Button("Settings");
    private VerticalLayout userField = new VerticalLayout();

    public ProjectView(ProjectViewPresenter projectViewPresenter)
    {
        layout.setSizeFull();
        this.presenter = projectViewPresenter;
        minimizeButton.setWidth(100,Unit.PERCENTAGE);
        menuBar.addComponent(minimizeButton);
        projectsButton.setWidth(100,Unit.PERCENTAGE);
        menuBar.addComponent(projectsButton);
        adminButton.setWidth(100,Unit.PERCENTAGE);
        menuBar.addComponent(adminButton);
        settingsButton.setWidth(100,Unit.PERCENTAGE);
        menuBar.addComponent(settingsButton);
        userField.setWidth(100,Unit.PERCENTAGE);
        userField.setHeight(30,Unit.PERCENTAGE);
        menuBar.addComponent(userField);
        menuBar.setComponentAlignment(userField, Alignment.BOTTOM_LEFT);
        menuBar.setSizeFull();
        menuBarouter.setHeight(100,Unit.PERCENTAGE);
        menuBarouter.setWidth(10,Unit.PERCENTAGE);
        menuBarouter.setContent(menuBar);

        layout.addComponent(menuBarouter);
        setCompositionRoot(layout);
    }

    @Override
    public void enter(ViewChangeListener.ViewChangeEvent viewChangeEvent) {

    }
}

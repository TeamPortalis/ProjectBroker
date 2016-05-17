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
    private HorizontalLayout layout = new HorizontalLayout();
    private VerticalLayout menuBar = new VerticalLayout();
    private Button minimizeButton = new Button("Minimize");
    private Button projectsButton = new Button("Projects");
    private Button adminButton = new Button("Admin");
    private Button settingsButton = new Button("Settings");
    private VerticalLayout userField = new VerticalLayout();
    private VerticalLayout searchArea = new VerticalLayout();
    private HorizontalLayout searchItems = new HorizontalLayout();
    private TextField searchBar = new TextField();
    private Button searchButton = new Button("Search");
    private ListSelect resultsList = new ListSelect();
    private NativeSelect filterSelection = new NativeSelect();


    public ProjectView(ProjectViewPresenter projectViewPresenter)
    {
        layout.setSizeFull();
        this.presenter = projectViewPresenter;

        //MenuBar
        menuBar.setSizeFull();
        minimizeButton.setStyleName("menu-button");
        menuBar.addComponent(minimizeButton);
        projectsButton.setStyleName("menu-button");
        menuBar.addComponent(projectsButton);
        adminButton.setStyleName("menu-button");
        menuBar.addComponent(adminButton);
        settingsButton.setStyleName("menu-button");
        menuBar.addComponent(settingsButton);
        userField.setHeight("30%");
        userField.setStyleName("menu-button");
        menuBar.addComponent(userField);
        menuBar.setComponentAlignment(userField, Alignment.BOTTOM_LEFT);


        //SearchArea
        searchArea.setSizeFull();
        searchItems.setStyleName("searchArea-items");
        filterSelection.addItem("Date");
        filterSelection.setStyleName("searchArea-items");
        resultsList.setStyleName("searchArea-items");
        searchItems.addComponents(searchBar, searchButton);
        searchArea.addComponents(searchItems, filterSelection, resultsList);


        layout.setSizeUndefined();
        layout.addComponent(menuBar);
        layout.addComponent(searchArea);
        layout.setExpandRatio(menuBar, 0.1f);
        layout.setExpandRatio(searchArea, 0.45f);
        setCompositionRoot(layout);
    }

    @Override
    public void enter(ViewChangeListener.ViewChangeEvent viewChangeEvent) {

    }
}

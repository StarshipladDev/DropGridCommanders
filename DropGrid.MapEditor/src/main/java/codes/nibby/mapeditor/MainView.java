package codes.nibby.mapeditor;

import javafx.scene.control.*;
import javafx.scene.layout.BorderPane;

public class MainView extends BorderPane {

    private ToolBar toolbar;
    private TabPane editorTabPane;

    public MainView() {
        toolbar = new ToolBar();
        setTop(toolbar);
        {
            Button btnNew = new Button("New");
            toolbar.getItems().add(btnNew);

            Button btnOpen = new Button("Open");
            toolbar.getItems().add(btnOpen);
        }

        editorTabPane = new TabPane();
        setCenter(editorTabPane);
    }

}

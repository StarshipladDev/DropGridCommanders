package codes.nibby.mapeditor;

import javafx.application.Application;
import javafx.scene.Scene;
import javafx.scene.control.Alert;
import javafx.scene.control.ButtonType;
import javafx.stage.DirectoryChooser;
import javafx.stage.Stage;

import java.io.File;
import java.io.IOException;

public class Main extends Application {

    public static void main(String[] args) {
        launch(args);
    }

    @Override
    public void start(Stage stage) throws Exception {
        loadPreference();
        loadImageAsset();

        Scene scene = new Scene(new MainView(), 800, 600);
        stage.setScene(scene);
        stage.setMinWidth(800);
        stage.setMinHeight(600);
        stage.show();
    }

    private void loadPreference() throws Exception {
        try {
            Preferences.load();
            if (Preferences.getContentFolder() == null) {
                promptSetContentFolder();
            }
        } catch (Exception e) {
            Preferences.delete();
            throw new Exception("An error occurred when loading preference data", e);
        }
    }

    private void loadImageAsset() throws Exception {
        try {
            ImageAssets.load();
        } catch (IOException e) {
            Preferences.delete();
            throw new Exception("An error occurred when loading image assets", e);
        }
    }

    @Override
    public void stop() throws Exception {
        Preferences.save();
        super.stop();
    }

    private void promptSetContentFolder() {
        Alert promptAlert = new Alert(Alert.AlertType.INFORMATION,
                "Perhaps this is the first time the application is launched, " +
                "DropGrid.Client/Content directory cannot be found automatically. " +
                "The next dialog will prompt you to locate the directory.",
                ButtonType.OK);
        promptAlert.showAndWait();

        boolean foundRightDirectory = false;
        while (!foundRightDirectory) {
            foundRightDirectory = promptContentFolderSelection();
        }
    }

    private boolean promptContentFolderSelection() {
        DirectoryChooser directoryChooser = new DirectoryChooser();
        File directory = new File(System.getProperty("user.dir"));
        directoryChooser.setInitialDirectory(directory);
        directoryChooser.setTitle("Select DropGrid.Client/Content Directory");
        File selectedFolder = directoryChooser.showDialog(null);
        if (selectedFolder == null) {
            try {
                stop();
            } catch (Exception e) {
                e.printStackTrace();
            }
            return false;
        }

        if (selectedFolder.getName().equals("Content") && selectedFolder.isDirectory()) {
            Preferences.setContentFolder(selectedFolder);
            return true;
        }
        return false;
    }
}

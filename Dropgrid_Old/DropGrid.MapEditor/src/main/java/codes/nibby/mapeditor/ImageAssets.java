package codes.nibby.mapeditor;

import javafx.scene.image.Image;

import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;

public class ImageAssets {

    private static Image TILESET;

    public Image tileset() {
        return TILESET;
    }

    public static void load() throws IOException {
        Path contentFolder = Preferences.getContentFolder();
        Path tilesetImageFile = contentFolder.resolve("tileset.png");
        TILESET = new Image(Files.newInputStream(tilesetImageFile));
    }

}

package codes.nibby.mapeditor.model;

import javafx.scene.image.Image;
import jdk.internal.jline.internal.Nullable;

public enum MapTile {

    NONE(0, null),

    ;

    private int id;
    private Image texture;

    MapTile(int id, @Nullable Image texture) {
        this.id = id;
        this.texture = texture;
    }

    public int getId() {
        return id;
    }

    public Image getTexture() {
        return texture;
    }
}

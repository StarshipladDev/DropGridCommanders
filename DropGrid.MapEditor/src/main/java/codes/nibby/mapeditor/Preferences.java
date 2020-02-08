package codes.nibby.mapeditor;

import java.io.BufferedWriter;
import java.io.File;
import java.io.IOException;
import java.io.PrintWriter;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.HashMap;
import java.util.Map;
import java.util.Scanner;

public class Preferences {

    private static Path CLIENT_CONTENT_FOLDER;
    private static Map<String, String> PREFERENCES = new HashMap<>();

    public static final String KEY_CONTENT_FOLDER = "contentFolder";

    static void load() throws IOException {
        loadPreferenceEntries();
        loadContentFolder();
    }

    private static void loadPreferenceEntries() throws IOException {
        Path preferenceFile = getPreferenceFile();
        if (!Files.exists(preferenceFile))
            return;

        try (Scanner scanner = new Scanner(Files.newInputStream(preferenceFile))) {
            while (scanner.hasNextLine()) {
                String readLine = scanner.nextLine();
                String[] keyValue = readLine.split(":");
                if (keyValue.length < 2) {
                    System.err.println("Malformed preference, ignoring: " + readLine);
                    continue;
                }

                String key = keyValue[0];
                String value = getConcatenated(keyValue);
                PREFERENCES.put(key, value);
            }
        }
    }

    private static Path getPreferenceFile() {
        return Paths.get(System.getProperty("user.dir")).resolve("prefs");
    }

    private static void loadContentFolder() {
        String contentFolderPrefValue = get(KEY_CONTENT_FOLDER);
        if (contentFolderPrefValue.isEmpty())
            return;
        CLIENT_CONTENT_FOLDER = Paths.get(contentFolderPrefValue);
    }

    public static Path getContentFolder() {
        return CLIENT_CONTENT_FOLDER;
    }

    public static String get(String preferenceKey) {
        return PREFERENCES.getOrDefault(preferenceKey, "");
    }

    private static String getConcatenated(String[] keyValue) {
        assert keyValue.length >= 2;

        StringBuilder result = new StringBuilder();
        for (int i = 1; i < keyValue.length; ++i) {
            result.append(keyValue[i]);
        }
        return result.toString();
    }

    public static void setContentFolder(File selectedFolder) {
        Path currentPath = Paths.get(System.getProperty("user.dir"));
        CLIENT_CONTENT_FOLDER = currentPath.relativize(selectedFolder.toPath());
        PREFERENCES.put(KEY_CONTENT_FOLDER, CLIENT_CONTENT_FOLDER.toString());
    }

    public static void save() throws IOException {
        Path preferenceFile = getPreferenceFile();

        try (PrintWriter writer = new PrintWriter(Files.newBufferedWriter(preferenceFile))) {
            PREFERENCES.entrySet().forEach(entry -> writeEntry(entry, writer));
        }
    }

    private static void writeEntry(Map.Entry<String, String> entry, PrintWriter writer) {
        writer.println(entry.getKey() + ":" + entry.getValue());
    }

    public static void delete() throws IOException {
        Path preferenceFile = getPreferenceFile();
        if (Files.exists(preferenceFile))
            Files.delete(preferenceFile);
    }
}

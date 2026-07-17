using System;
using System.Diagnostics;
using System.IO;
using System.Media;
using System.Threading;
using System.Windows.Forms;
using SharpCompress.Archives;
using SharpCompress.Common;
using SharpCompress.Readers;
using System.Linq;
using System.ComponentModel.Design;

namespace CivClassicLauncher

// ====================================================
// Main Launcher Window
//
// This is the main entry point for the launcher.
// It handles:
// - Game launching
// - Mod management
// - Launcher settings
// - Music
// - Profiles
// - Metadata editing
// - Update checking
// ====================================================

{
    public partial class Form1 : Form
    {
        // ----------------------------------------------------
        // Launcher State
        // Stores music, preview images and other launcher data.
        // ----------------------------------------------------

        private SoundPlayer? player;
        private bool ShuffleMusic = true;
        private string previewImageFile = "";

        //Form1 Setup
        public Form1()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            this.KeyPreview = true;
        }

// ----------------------------------------------------
// Launcher startup.
//
// Initialises the launcher by:
// - Loading settings
// - Populating combo boxes
// - Loading installed mods
// - Loading profiles
// - Checking for updates
// ----------------------------------------------------

private async void Form1_Load(object sender, EventArgs e)
        {
            Directory.SetCurrentDirectory(
                AppDomain.CurrentDomain.BaseDirectory
            );

            // Populate Mod Game Dropdown
            comboBoxGame.Items.Add("Civ1");
            comboBoxGame.Items.Add("Civ2MGE");
            comboBoxGame.Items.Add("Civ2ToT");

            comboBoxGame.SelectedIndex = 1;

            comboBoxModCategory.Items.Add("Graphics Mod");
            comboBoxModCategory.Items.Add("Rules Mod");
            comboBoxModCategory.Items.Add("Scenario");
            comboBoxModCategory.Items.Add("Total Conversion");
            comboBoxModCategory.Items.Add("Utility");
            comboBoxModCategory.Items.Add("Other");

            comboBoxModCategory.SelectedIndex = 0;

            comboGameFilter.Items.Add("All");
            comboGameFilter.Items.Add("Civ1");
            comboGameFilter.Items.Add("Civ2MGE");
            comboGameFilter.Items.Add("Civ2ToT");

            comboGameFilter.SelectedIndex = 0;

            // Load Launcher Settings
            LoadSettings();
            if (checkBox1.Checked)
            {
                PlayRandomMusic();
            }
            // LOAD MODS
            LoadMods();

            Directory.CreateDirectory(ProfilesFolder);
            LoadProfiles();


            // Settings Panel Visibility
            panel1.Visible = false;
            panelDebugMenu.Visible = false;

            // Populate Mod Labels
            labelModName.Text = "Name:";
            labelModGame.Text = "Game:";
            labelModCategory.Text = "Type:";
            labelModImported.Text = "Imported:";


            // Check GitHub for a newer launcher version.
            // If one exists we download the version file
            // and display the update window.

            string? latest = await UpdateChecker.GetLatestVersionAsync();

            if (latest != null)
            {
                Version currentVersion =
                    System.Reflection.Assembly
                    .GetExecutingAssembly()
                    .GetName()
                    .Version!;

                Version latestVersion = new Version(latest);

                if (latestVersion > currentVersion)
                {
                    string? versionFile =
                        await UpdateChecker.GetVersionFileAsync();

                    string download = "";
                    string archive = "";
                    string changelog = "";
                    string releaseDate = "";

                    bool readingChangelog = false;

                    foreach (string line in versionFile.Split('\n'))
                    {
                        string trimmed = line.TrimEnd();

                        if (trimmed.StartsWith("Version="))
                        {
                            readingChangelog = false;
                            continue;
                        }

                        if (trimmed.StartsWith("Date="))
                        {
                            releaseDate = trimmed.Substring(5).Trim();
                            readingChangelog = false;
                            continue;
                        }

                        if (trimmed.StartsWith("Download="))
                        {
                            download = trimmed.Substring(9).Trim();
                            readingChangelog = false;
                            continue;
                        }

                        if (trimmed.StartsWith("Archive="))
                        {
                            archive = trimmed.Substring(8).Trim();
                            readingChangelog = false;
                            continue;
                        }

                        if (trimmed.StartsWith("Changelog="))
                        {
                            changelog = trimmed.Substring(10).Trim();
                            readingChangelog = true;
                            continue;
                        }

                        if (readingChangelog)
                        {
                            changelog += Environment.NewLine + trimmed;
                        }
                    }

                    using (UpdateForm form = new UpdateForm(
                        currentVersion,
                        latestVersion,
                        releaseDate,
                        changelog,
                        download,
                        archive))
                    {
                        form.ShowDialog();
                    }
                }
            }
        }
    // Referencing my settings ini file for the launcher settings
        private readonly string SettingsFile =
    Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        "settings.ini"
    );

    // Favourites.ini 
        private readonly string FavoritesFile =
    Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        "Favorites.ini"
    );

        private readonly string ProfilesFolder =
    Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        "Profiles"
    );

        // Play Civ2MGE Enhanced
        private void button1_Click(object sender, EventArgs e)
        {
            Launcher.StartMGEEnhanced();

            Thread.Sleep(100);

            SendKeys.SendWait("{ENTER}");

            Application.Exit();
        }

        // Play Civ2MGE Original
        private void button2_Click(object sender, EventArgs e)
        {
            Launcher.StartMGEOriginal();

            Application.Exit();
        }

        // Play Civilization
        private void button4_Click(object sender, EventArgs e)
        {
            Launcher.StartCiv1();

            Application.Exit();
        }

        // Play Civ2 Test of Time
        private void button5_Click(object sender, EventArgs e)
        {
            Launcher.StartToT();

            Application.Exit();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            //  Launcher.Quit(); blank
        }

        // Quit
        private void label2_Click(object sender, EventArgs e)
        {

        }

        // Manuals
        private void label3_Click(object sender, EventArgs e)
        {
            Launcher.OpenDocumentation();
        }

        // Open Saves
        private void label4_Click(object sender, EventArgs e)
        {
            Launcher.OpenSaves();
        }

        // Open Mod Folder
        private void label5_Click(object sender, EventArgs e)
        {
            Launcher.OpenMods();
        }

        // Launcher, Play random music track from our Launcher Music folder
        private void PlayRandomMusic()
        {
            string musicFolder =
                Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "Music",
                    "Launcher"
                    );

            if (!
                Directory.Exists(musicFolder))
                return;

            string[] files =
                Directory.GetFiles(
                    musicFolder,
                    "*.wav");

            if (files.Length == 0) return;

            Random rng = new Random();

            string track = files[rng.Next(files.Length)];

            player = new
                SoundPlayer(track);

            player.PlayLooping();
        }

        protected override void
            OnFormClosing(FormClosingEventArgs e)
        {
            player?.Stop();

            base.OnFormClosing(e);
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
        // Open Collection Folder (Settings)
        private void button6_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo()
            {
                FileName = AppDomain.CurrentDomain.BaseDirectory,
                UseShellExecute = true
            });
        }
        // Checkbox1 is for Enabling Music
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            SaveSettings();

            if (checkBox1.Checked)
            {
                PlayRandomMusic();
            }
            else
            {
                player?.Stop();
            }
        }
        // Checkbox2 is for Shuffling Music
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            ShuffleMusic = checkBox2.Checked;

            SaveSettings();
        }
        // Settings Panel Visibility and stuffs
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        // Cog Icon for our settings panel
        private void button3_Click(object sender, EventArgs e)
        {
            panel1.Visible = !panel1.Visible;
        }

        // ====================================================
        // Launcher Settings
        // ====================================================


        // Load Launcher Settings
        private void LoadSettings()
        {
            if (!File.Exists(SettingsFile))
                return;

            string[] lines = File.ReadAllLines(SettingsFile);

            foreach (string line in lines)
            {
                if (line.StartsWith("MusicEnabled="))
                {
                    checkBox1.Checked =
                        bool.Parse(
                            line.Substring("MusicEnabled=".Length)
                        );
                }

                if (line.StartsWith("ShuffleMusic="))
                {
                    checkBox2.Checked =
                        bool.Parse(
                            line.Substring("ShuffleMusic=".Length)
                        );
                }
            }
        }

        // Save Settings for the launcher (Music options currently)

        private void SaveSettings()
        {
            File.WriteAllLines(
                SettingsFile,
                new string[]
                {
            $"MusicEnabled={checkBox1.Checked}",
            $"ShuffleMusic={checkBox2.Checked}"
                }
            );
        }

        // Load Favourites
        private HashSet<string> LoadFavorites()
        {
            if (!File.Exists(FavoritesFile))
                return new HashSet<string>();

            return File.ReadAllLines(FavoritesFile)
                .ToHashSet(StringComparer.OrdinalIgnoreCase);
        }

        //Save Favourites
        private void SaveFavorites(HashSet<string> favorites)
        {
            File.WriteAllLines(FavoritesFile, favorites);
        }

        // Import Mod Button (Settings)
        private async void button7_Click(
            object sender,
            EventArgs e)
        {
            await ImportMod();
        }

        // ----------------------------------------------------
        // Import a mod archive.
        // Import Mod Functionality using SharpCompress libraries to extract the most common mod archives.
        // Supports ZIP, 7Z and RAR archives.
        // Extracts the archive, generates a default mod.ini
        // and refreshes the installed mod list.
        // ----------------------------------------------------
        private async Task ImportMod()
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filter =
                "Archives (*.zip;*.7z;*.rar)|*.zip;*.7z;*.rar";

            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            string archivePath = dialog.FileName;

            string modName =
                Path.GetFileNameWithoutExtension(
                    archivePath
                );
            if (comboBoxGame.SelectedItem == null)
            {
                MessageBox.Show(
                    "Please select a game first."
                );

                return;
            }
            if (comboBoxModCategory.SelectedItem == null)
            {
                MessageBox.Show(
                    "Please select a mod type."
                );

                return;
            }

            string selectedGame =
                comboBoxGame.SelectedItem.ToString();

            string selectedType =
                 comboBoxModCategory.SelectedItem.ToString();

            string targetFolder =
                Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "Mods",
                    selectedGame,
                    modName
                );

            if (Directory.Exists(targetFolder))
            {
                MessageBox.Show(
                    "This mod is already installed."
                );

                return;
            }

            progressBarImport.Visible = true;
            progressBarImport.Value = 0;

            Directory.CreateDirectory(targetFolder);

            using var archive =
                ArchiveFactory.OpenArchive(archivePath);

            var entries =
                archive.Entries
                    .Where(e => !e.IsDirectory)
                    .ToList();

            int totalFiles = entries.Count;
            int currentFile = 0;

            await Task.Run(() =>
            {
                foreach (var entry in entries)
                {
                    entry.WriteToDirectory(
                        targetFolder,
                        new ExtractionOptions()
                        {
                            ExtractFullPath = true,
                            Overwrite = true
                        });

                    currentFile++;

                    int percent =
                        (int)(
                            (currentFile * 100.0)
                            / totalFiles
                        );

                    Invoke(() =>
                    {
                        progressBarImport.Value =
                            Math.Min(percent, 100);
                    });
                }
            });

            MergeLegacyExtraFolders(targetFolder);

            GenerateModIni(
                targetFolder,
                modName,
                selectedGame,
                selectedType
            );

            LoadMods();

            progressBarImport.Visible = false;

            MessageBox.Show(
                "Mod imported successfully.",
                "Civilization Classics Collection"
            );
        }

        // Generate mod metadata used by the launcher.
        // Stores game type, mod category and import information.
        // Every imported mod receives a standard metadata file.
        // This allows the launcher to treat all mods consistently.
        private void GenerateModIni(
            string folder,
            string modName,
            string game,
            string type)
        {
            string iniPath =
                Path.Combine(
                    folder,
                    "mod.ini"
                );

            File.WriteAllLines(
                iniPath,
                new string[]
                {
            "[Mod]",

            $"Name={modName}",
            $"Game={game}",
            $"Type={type}",
            "Author=Unknown",
            "Version=1.0",
            "Description=",
            "Website=",
            "PreviewImage=",
            "CompatibleLauncher=0.6",
            $"Imported={DateTime.Now}",
            $"UniqueID={Guid.NewGuid()}"
                }
            );
        }

        private bool IsLegacyExtraFolder(string folderName)
        {
            string name =
                folderName
                    .ToLowerInvariant()
                    .Replace("_", "")
                    .Replace(" ", "");

            return name.Contains("extrafiles");
        }

        // Older Civilization II mods sometimes store file inside an "Extra Files" folder.
        // We merge those folders into the main mod folder so they behave like modern launcher compatible mods.

        private void MergeLegacyExtraFolders(string modFolder)
        {
            foreach (string folder in Directory.GetDirectories(
                modFolder,
                "*",
                SearchOption.AllDirectories))
            {
                string folderName =
                    Path.GetFileName(folder);

                if (!IsLegacyExtraFolder(folderName))
                    continue;

                Debug.WriteLine(
                    $"Legacy mod structure detected: {folder}"
                );

                foreach (string entry in Directory.GetFileSystemEntries(folder))
                {
                    string destination =
                        Path.Combine(
                            modFolder,
                            Path.GetFileName(entry)
                        );

                    if (Directory.Exists(entry))
                    {
                        CopyDirectory(entry, destination);
                    }
                    else
                    {
                        File.Copy(
                            entry,
                            destination,
                            true
                        );
                    }
                }

                Directory.Delete(
                    folder,
                    true
                );

                Debug.WriteLine(
                    "Legacy extra files merged successfully."
                );
            }
        }

        private void CopyDirectory(
    string source,
    string destination)
        {
            Directory.CreateDirectory(destination);

            foreach (string file in Directory.GetFiles(source))
            {
                File.Copy(
                    file,
                    Path.Combine(
                        destination,
                        Path.GetFileName(file)
                    ),
                    true
                );
            }

            foreach (string dir in Directory.GetDirectories(source))
            {
                CopyDirectory(
                    dir,
                    Path.Combine(
                        destination,
                        Path.GetFileName(dir)
                    )
                );
            }
        }

        // LOAD MODS
        // Rebuilds the installed mod list.
        //
        // Applies game filtering, favourites filtering and refreshes the metadata panel.
        private void LoadMods()
        {
            checkedListBoxMods.Items.Clear();

            // Clear previous metadata
            textBoxAuthor.Clear();
            textBoxVersion.Clear();
            textBoxWebsite.Clear();
            textBoxDescription.Clear();

            string selectedGame = "All";

            string modsPath =
                Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "Mods"
                );

            if (!Directory.Exists(modsPath))
                return;

            string[] modFiles =
                Directory.GetFiles(
                    modsPath,
                    "mod.ini",
                    SearchOption.AllDirectories
                );

            if (comboGameFilter.SelectedItem != null)
            {
                selectedGame = comboGameFilter.SelectedItem.ToString();
            }

            HashSet<string> favorites = LoadFavorites();

            foreach (string modIni in modFiles)
            {
                string relativePath =
                    Path.GetRelativePath(
                        modsPath,
                        Path.GetDirectoryName(modIni)
                    );

                if (checkBoxFavorites.Checked &&
                    !favorites.Contains(relativePath))
                {
                    continue;
                }

                string game = GetModGame(relativePath);

                if (selectedGame == "All" || game == selectedGame)
                {
                    string displayName = relativePath;

                    if (favorites.Contains(relativePath))
                    {
                        displayName = "⭐ " + displayName;
                    }

                    checkedListBoxMods.Items.Add(displayName);
                }
            }
        }


        private void comboBoxGame_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBoxMods_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadModDetails();

        }

        private void LoadPreviewImage(string imagePath)
        {
            if (pictureBoxPreview.Image != null)
            {
                pictureBoxPreview.Image.Dispose();
                pictureBoxPreview.Image = null;
            }

            if (!File.Exists(imagePath))
                return;

            using (FileStream stream = new FileStream(
                imagePath,
                FileMode.Open,
                FileAccess.Read))
            {
                pictureBoxPreview.Image =
                    Image.FromStream(stream).Clone() as Image;
            }
        }


        // Load Mod Details: Grabs the details automatically created when we setup and import a mod.
        // This is essential for correct labeling and folder application.
        private void LoadModDetails()
        {
            if (checkedListBoxMods.SelectedItem == null)
                return;

            string modsPath =
                Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "Mods"
                );

            string selectedMod = GetSelectedMod();

            string modFolder =
                Path.Combine(
                    modsPath,
                    selectedMod
                );

            string iniPath =
                Path.Combine(
                    modFolder,
                    "mod.ini"
                );

            if (!File.Exists(iniPath))
                return;

            string[] lines =
                File.ReadAllLines(iniPath);

            foreach (string line in lines)
            {
                if (line.StartsWith("Name="))
                {
                    labelModName.Text =
                        "Name: " +
                        line.Substring(5);
                }

                if (line.StartsWith("Game="))
                {
                    labelModGame.Text =
                        "Game: " +
                        line.Substring(5);
                }

                if (line.StartsWith("Type="))
                {
                    labelModCategory.Text =
                        "Type: " +
                        line.Substring(5);
                }

                if (line.StartsWith("Imported="))
                {
                    labelModImported.Text =
                        "Imported: " +
                        line.Substring(9);
                }

                if (line.StartsWith("Author="))
                {
                    textBoxAuthor.Text =
                        line.Substring(7);
                }

                if (line.StartsWith("Version="))
                {
                    textBoxVersion.Text =
                        line.Substring(8);
                }

                if (line.StartsWith("Website="))
                {
                    textBoxWebsite.Text =
                        line.Substring(8);
                }

                if (line.StartsWith("PreviewImage="))
                {
                    string imageName = line.Substring(13);

                    if (!string.IsNullOrWhiteSpace(imageName))
                    {
                        string imagePath =
                            Path.Combine(modFolder, imageName);

                        if (File.Exists(imagePath))
                        {
                            LoadPreviewImage(imagePath);
                        }
                        else
                        {
                            pictureBoxPreview.Image = null;
                        }
                    }
                    else
                    {
                        pictureBoxPreview.Image = null;
                    }

                }

                if (line.StartsWith("Description="))
                {
                    textBoxDescription.Text =
                        line.Substring(12);
                }
            }
            HashSet<string> favorites = LoadFavorites();

            buttonFavorite.Text =
                favorites.Contains(selectedMod)
                ? "★ Remove Favourite"
                : "☆ Add Favourite";


        }
        // Delete the selected mod and refresh the installed mod list.
        private void DeleteSelectedMod()
        {
            if (checkedListBoxMods.SelectedItem == null)
            {
                MessageBox.Show(
                    "Please select a mod first."
                );

                return;
            }

            string selectedMod =
                checkedListBoxMods.SelectedItem.ToString();

            DialogResult result =
                MessageBox.Show(
                    $"Delete mod '{selectedMod}'?",
                    "Delete Mod",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

            if (result != DialogResult.Yes)
                return;

            string modFolder =
                Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "Mods",
                    selectedMod
                );

            if (Directory.Exists(modFolder))
            {
                Directory.Delete(
                    modFolder,
                    true
                );
            }

            LoadMods();

            labelModName.Text = "Name:";
            labelModGame.Text = "Game:";
            labelModCategory.Text = "Type:";
            labelModImported.Text = "Imported:";

            MessageBox.Show(
                "Mod deleted."
            );
        }

        // ====================================================
        // Launch Selected Mods
        //
        // This is the main deployment pipeline.
        //
        // The launcher:
        // 1. Validates the selected mods
        // 2. Creates a backup
        // 3. Applies the selected mods
        // 4. Launches the game
        // 5. Restores the original files afterwards
        // ====================================================
        private void LaunchSelectedMod()
        {
            labelStatus.Visible = true;
            labelStatus.Text = "Preparing selected mods...";
            Application.DoEvents();

            if (
    checkedListBoxMods.CheckedItems.Count == 0
)
            {
                MessageBox.Show(
                    "Please select at least one mod."
                );

                return;
            }
            if (checkedListBoxMods.SelectedItem == null)
            {
                MessageBox.Show(
                    "Please select a mod first."
                );

                return;
            }

            List<string> enabledMods =
              new List<string>();

            foreach (
                object item
                in checkedListBoxMods.CheckedItems)
            {
                enabledMods.Add(
                    item.ToString()
                );
            }

            string game =
                GetModGame(
                    enabledMods[0]
                );

            foreach (string mod in enabledMods)
            {
                if (
                    GetModGame(mod)
                    != game
                )
                {
                    MessageBox.Show(
                        "All selected mods must target the same game."
                    );

                    return;
                }
            }

            BackupModFiles();

            labelStatus.Text =
             $"Applying {enabledMods.Count} mod(s)...";
            Application.DoEvents();

            foreach (string mod in enabledMods)
            {
                ApplyMod(mod);
            }

            labelStatus.Text =
             "Launching game...";
            Application.DoEvents();

            switch (game)
            {
                case "Civ1":
                    Launcher.StartCiv1();
                    break;

                case "Civ2MGE":
                    Launcher.StartMGEEnhanced();
                    Thread.Sleep(100);
                    SendKeys.SendWait("{ENTER}");
                    WatchForGameExit();
                    break;

                case "Civ2ToT":
                    Launcher.StartToT();
                    WatchForGameExit();
                    break;

                default:
                    MessageBox.Show(
                        $"Unknown game type: {game}"
                    );
                    break;

                    labelStatus.Text =
                         "Restoring original files...";
                         Application.DoEvents();
            }
        }


    // Hidden debug menu.
    // Enter the Konami Code to toggle the developer tools in the mod manager window.
    // This is mainly used during development and testing.
    
        private int konamiIndex = 0;

        private readonly Keys[] konamiCode =
        {
    Keys.Up,
    Keys.Up,
    Keys.Down,
    Keys.Down,
    Keys.Left,
    Keys.Right,
    Keys.Left,
    Keys.Right,
    Keys.B,
    Keys.A
};

        protected override bool ProcessCmdKey(
            ref Message msg,
            Keys keyData)
        {
            if (keyData == konamiCode[konamiIndex])
            {
                konamiIndex++;

                if (konamiIndex >= konamiCode.Length)
                {
                    panelDebugMenu.Visible =
                        !panelDebugMenu.Visible;

                    if (panelDebugMenu.Visible)
                    {
                        panelDebugMenu.BringToFront();
                    }

                    konamiIndex = 0;
                }
            }
            else
            {
                konamiIndex = 0;
            }

            return base.ProcessCmdKey(
                ref msg,
                keyData
            );
            MessageBox.Show(keyData.ToString());

            labelStatus.Text =
    "Backing up files...";
            Application.DoEvents();
        }

        // DEBUG MENU OPTION: See a list of what changes would be made by applying a certain mod.
        private void PreviewModChanges()
        {
            if (checkedListBoxMods.SelectedItem == null)
            {
                MessageBox.Show(
                    "Please select a mod first."
                );

                return;
            }

            List<string> enabledMods =
                new List<string>();

            foreach (object item in checkedListBoxMods.CheckedItems)
            {
                enabledMods.Add(
                    item.ToString()
                );
            }
            string game =
            GetModGame(
            enabledMods[0]
             );

            string gameFolder =
                Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    game
                );

            List<string> replacements =
                new List<string>();
            string selectedMod =
             enabledMods[0];

            string modsPath =
                Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "Mods"
                );

            string modFolder =
                Path.Combine(
                    modsPath,
                    selectedMod
                );

            string deploymentRoot =
                modFolder;
            string[] childFolders =
                Directory.GetDirectories(
                    modFolder
                );

            if (childFolders.Length == 1)
            {
                string nestedFolder =
                    childFolders[0];

                if (
                    Directory.Exists(
                        Path.Combine(
                            nestedFolder,
                            "Original"
                        )
                    )
                    ||
                    Directory.Exists(
                        Path.Combine(
                            nestedFolder,
                            "Sound"
                        )
                    )
                    ||
                    Directory.Exists(
                        Path.Combine(
                            nestedFolder,
                            "Video"
                        )
                    )
                )
                {
                    deploymentRoot =
                        nestedFolder;
                }
            }

            string[] modFiles =
                   Directory.GetFiles(
                   deploymentRoot,
                    "*.*",
                    SearchOption.AllDirectories
                );

            foreach (string modFile in modFiles)
            {
                string relativePath =
                Path.GetRelativePath(
                deploymentRoot,
                        modFile
                    );

                if (
                    relativePath.Equals(
                        "mod.ini",
                        StringComparison.OrdinalIgnoreCase
                    ))
                    continue;

                string targetFile =
                    Path.Combine(
                        gameFolder,
                        relativePath
                    );

                if (File.Exists(targetFile))
                {
                    replacements.Add(relativePath);
                }
            }

            if (replacements.Count == 0)
            {
                MessageBox.Show(
                    "No matching files found.",
                    "Preview Changes"
                );

                return;
            }

            string report =
                string.Join(
                    Environment.NewLine,
                    replacements
                );

            MessageBox.Show(
                $"Files that will be replaced:\n\n" +
                report +
                $"\n\nTotal Files: {replacements.Count}",
                "Preview Changes"
            );
        }

        // Step 1 - Backup original files

        // Backup Mod Files (The ones that would be replaced from the game dir.) // DEBUG MENU OPTION
        // Only backs up files that will be overwritten by the selected mod.
        // Backup is stored in Temp\ActiveModBackup and restored on game exit.

        private void BackupModFiles()
        {
            HashSet<string> filesToBackup =
              new HashSet<string>();
            if (checkedListBoxMods.CheckedItems.Count == 0)
            {
                MessageBox.Show(
                    "Please select at least one mod."
                );

                return;
            }

            List<string> enabledMods =
                new List<string>();

            foreach (object item in checkedListBoxMods.CheckedItems)
            {
                enabledMods.Add(
                    item.ToString()
                );
            }

            string modsPath =
                 Path.Combine(
                 AppDomain.CurrentDomain.BaseDirectory,
                 "Mods"
                  );

            string game =
                GetModGame(
                    enabledMods[0]
                );

            string gameFolder =
                Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    game
                );

            string backupFolder =
                Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "Temp",
                    "ActiveModBackup"
                );

            if (Directory.Exists(backupFolder))
            {
                foreach (string file in Directory.GetFiles(
                    backupFolder,
                    "*",
                    SearchOption.AllDirectories))
                {
                    File.SetAttributes(
                        file,
                        FileAttributes.Normal
                    );
                }

                Directory.Delete(
                    backupFolder,
                    true
                );
            }

            Directory.CreateDirectory(
                backupFolder
            );
            int backupCount = 0;

            foreach (string selectedMod in enabledMods)
            {


                string modFolder =
                 Path.Combine(
                     modsPath,
                         selectedMod
                     );
                string deploymentRoot =
                     modFolder;

                string[] childFolders =
                    Directory.GetDirectories(
                        modFolder
                    );

                if (childFolders.Length == 1)
                {
                    string nestedFolder =
                        childFolders[0];

                    if (
                        Directory.Exists(
                            Path.Combine(
                                nestedFolder,
                                "Original"
                            )
                        )
                        ||
                        Directory.Exists(
                            Path.Combine(
                                nestedFolder,
                                "Sound"
                            )
                        )
                        ||
                        Directory.Exists(
                            Path.Combine(
                                nestedFolder,
                                "Video"
                            )
                        )
                    )
                    {
                        deploymentRoot =
                            nestedFolder;
                    }
                }
                string[] modFiles =
                    Directory.GetFiles(
                     deploymentRoot,
                      "*.*",
                      SearchOption.AllDirectories
                  );


                foreach (string modFile in modFiles)
                {
                    string relativePath =
                        Path.GetRelativePath(
                             deploymentRoot,
                              modFile
                        );

                    if (
                        relativePath.Equals(
                            "mod.ini",
                            StringComparison.OrdinalIgnoreCase
                        ))
                        continue;

                    string originalFile =
                        Path.Combine(
                            gameFolder,
                            relativePath
                        );

                    if (!File.Exists(originalFile))
                        continue;

                    string backupFile =
                        Path.Combine(
                            backupFolder,
                            relativePath
                        );

                    if (File.Exists(backupFile))
                        continue;

                    Directory.CreateDirectory(
                        Path.GetDirectoryName(
                            backupFile
                        )
                    );

                    File.Copy(
                        originalFile,
                        backupFile,
                        true
                    );

                    backupCount++;
                }
            }

            labelStatus.Text =
        "Backing up files...";
            Application.DoEvents();
        }

        // Step 2 - Deploy selected mods

        // Apply Mod to the game directory // DEBUG MENU OPTION
        // Copies matching files from the mod folder into the target game directory.
        // Files that do not exist in the game directory are ignored.
        private void ApplyMod(
            string selectedMod)
        {

            string modsPath =
                Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "Mods"
                );

            string modFolder =
                Path.Combine(
                    modsPath,
                    selectedMod
                );

            string iniPath =
                Path.Combine(
                    modFolder,
                    "mod.ini"
                );

            string game = "Unknown";

            foreach (string line in File.ReadAllLines(iniPath))
            {
                if (line.StartsWith("Game="))
                {
                    game = line.Substring(5);
                }
            }

            string gameFolder =
                Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    game
                );

            string deploymentRoot =
    modFolder;

            string[] childFolders =
                Directory.GetDirectories(
                    modFolder
                );

            if (childFolders.Length == 1)
            {
                string nestedFolder =
                    childFolders[0];

                if (
                    Directory.Exists(
                        Path.Combine(
                            nestedFolder,
                            "Original"
                        )
                    )
                    ||
                    Directory.Exists(
                        Path.Combine(
                            nestedFolder,
                            "Sound"
                        )
                    )
                    ||
                    Directory.Exists(
                        Path.Combine(
                            nestedFolder,
                            "Video"
                        )
                    )
                )
                {
                    deploymentRoot =
                        nestedFolder;
                }
            }

            string[] modFiles =
Directory.GetFiles(
    deploymentRoot,
                    "*.*",
                    SearchOption.AllDirectories
                );

            int copiedCount = 0;

            foreach (string modFile in modFiles)
            {
                string fileName =
                    Path.GetFileName(
                        modFile
                    );

                if (
                    fileName.Equals(
                        "civ2.exe",
                        StringComparison.OrdinalIgnoreCase
                    )
                    ||
                    fileName.Equals(
                        "civ2tot.exe",
                        StringComparison.OrdinalIgnoreCase
                    )
                )
                {
                    continue;
                }
                string relativePath =
Path.GetRelativePath(
    deploymentRoot,
                        modFile
                    );

                if (
                    relativePath.Equals(
                        "mod.ini",
                        StringComparison.OrdinalIgnoreCase
                    ))
                    continue;

                string targetFile =
                    Path.Combine(
                        gameFolder,
                        relativePath
                    );

                if (File.Exists(targetFile))
                {
                    File.Copy(
                        modFile,
                        targetFile,
                        true
                    );
                    copiedCount++;
                }
            }
        }

        // Step 3 - Restore original files

        //Restore Backed Up Original Game Files // DEBUG MENU OPTION
        // Restores original game files from the active backup.
        // Used automatically after game exit and manually through the debug tools.
        private void RestoreBackup()
        {
            string backupFolder =
                Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "Temp",
                    "ActiveModBackup"
                );

            if (!Directory.Exists(backupFolder))
            {
                MessageBox.Show(
                    "No backup found."
                );

                return;
            }

            if (checkedListBoxMods.SelectedItem == null)
            {
                MessageBox.Show(
                    "Please select a mod first."
                );

                return;
            }

            string selectedMod =
                checkedListBoxMods.SelectedItem.ToString();

            string modFolder =
                Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "Mods",
                    selectedMod
                );

            string iniPath =
                Path.Combine(
                    modFolder,
                    "mod.ini"
                );

            string game = "Unknown";

            foreach (string line in File.ReadAllLines(iniPath))
            {
                if (line.StartsWith("Game="))
                {
                    game =
                        line.Substring(5);
                }
            }

            string gameFolder =
                Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    game
                );

            string[] backupFiles =
                Directory.GetFiles(
                    backupFolder,
                    "*.*",
                    SearchOption.AllDirectories
                );

            int restoreCount = 0;

            foreach (string backupFile in backupFiles)
            {
                string relativePath =
                    Path.GetRelativePath(
                        backupFolder,
                        backupFile
                    );

                string targetFile =
                    Path.Combine(
                        gameFolder,
                        relativePath
                    );

                Directory.CreateDirectory(
                    Path.GetDirectoryName(
                        targetFile
                    )
                );

                File.Copy(
                    backupFile,
                    targetFile,
                    true
                );

                restoreCount++;
            }
        }

        // Watcher (Looks for the game executables and 'watches' when it exits) part of the mod automated backup restore
        private async void WatchForGameExit()
        {
            await Task.Run(() =>
            {
                int timeout = 30;

                // Wait for Civ2 to start

                while (
                    Process.GetProcessesByName("civ2")
                        .Length == 0
                    &&
                    timeout > 0
                )
                {
                    Thread.Sleep(1000);

                    timeout--;
                }

                if (timeout == 0)
                {
                    RestoreBackup();

                    labelStatus.Text =
                        "Game launch cancelled. Files restored.";

                    return;
                }

                // Now wait for Civ2 to close

                while (
                    Process.GetProcessesByName("civ2")
                        .Length > 0
                )
                {
                    Thread.Sleep(1000);
                }
            });

            RestoreBackup();

            labelStatus.Text =
                "Original files restored.";
        }

        // On click command calls
        private void deleteMod_Click(object sender, EventArgs e)
        {
            DeleteSelectedMod();
        }

        private void launchMod_Click(object sender, EventArgs e)
        {
            LaunchSelectedMod();
        }

        private void previewChanges_Click(object sender, EventArgs e)
        {
            PreviewModChanges();
        }

        private void backupTest_Click(object sender, EventArgs e)
        {
            BackupModFiles();
        }

        private void applyModTest_Click(
            object sender,
            EventArgs e)
        {
            if (
                checkedListBoxMods.SelectedItem
                == null
            )
            {
                MessageBox.Show(
                    "Please select a mod first."
                );

                return;
            }

            ApplyMod(
                checkedListBoxMods
                    .SelectedItem
                    .ToString()
            );
        }
        private void restoreBackup_Click(object sender, EventArgs e)
        {
            RestoreBackup();
        }

        // Combo Box for mod Category (Might use later when I add auto ModType detection)
        private void comboBoxModCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        // Open Mod Guide txt file from documentation
        private void buttonModGuide_Click(object sender, EventArgs e)
        {
            string guidePath =
    Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        "Documentation",
        "How To Use The Mod Menu.txt"
    );

            if (File.Exists(guidePath))
            {
                Process.Start(
                    new ProcessStartInfo()
                    {
                        FileName = guidePath,
                        UseShellExecute = true
                    }
                );
            }
            else
            {
                MessageBox.Show(
                    "Mod guide not found."
                );
            }
        }

        private string GetModGame(
    string selectedMod)
        {
            string iniPath =
                Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "Mods",
                    selectedMod,
                    "mod.ini"
                );

            if (!File.Exists(iniPath))
                return "Unknown";

            foreach (string line in File.ReadAllLines(iniPath))
            {
                if (line.StartsWith("Game="))
                {
                    return line.Substring(5);
                }
            }

            return "Unknown";
        }

        private string GetSelectedMod()
        {
            if (checkedListBoxMods.SelectedItem == null)
                return "";

            string selected =
                checkedListBoxMods.SelectedItem.ToString();

            if (selected.StartsWith("⭐ "))
            {
                selected = selected.Substring(2);
            }

            return selected;
        }

        private void labelModName_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void panelModManager_Paint(object sender, PaintEventArgs e)
        {

        }

        private void buttonModManager_Click(object sender, EventArgs e)
        {
            panelMain.Visible = false;
            panelModManager.Visible = true;
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            panelModManager.Visible = false;
            panelMain.Visible = true;
        }

        private void buttonBack_Click_1(object sender, EventArgs e)
        {
            panelModManager.Visible = false;
            panelMain.Visible = true;
        }

        //New quit button
        private void button8_Click(object sender, EventArgs e)
        {
            Launcher.Quit();
        }

        private void buttonCloseSettings_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void comboGameFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadMods();
        }

        private void buttonFavorite_Click(object sender, EventArgs e)
        {
            if (checkedListBoxMods.SelectedItem == null)
            {
                MessageBox.Show("Please select a mod first.");
                return;
            }

            HashSet<string> favorites = LoadFavorites();

            string selectedMod = GetSelectedMod();

            if (favorites.Contains(selectedMod))
            {
                favorites.Remove(selectedMod);
                buttonFavorite.Text = "☆ Add Favourite";
            }
            else
            {
                favorites.Add(selectedMod);
                buttonFavorite.Text = "★ Remove Favourite";
            }

            SaveFavorites(favorites);

            // Refresh the list so the star appears/disappears
            LoadMods();
        }

        private void checkBoxFavorites_CheckedChanged(object sender, EventArgs e)
        {
            LoadMods();
        }

        private void LoadProfiles()
        {
            listBoxProfiles.Items.Clear();

            if (!Directory.Exists(ProfilesFolder))
                return;

            string[] files =
                Directory.GetFiles(
                    ProfilesFolder,
                    "*.ini"
                );

            foreach (string file in files)
            {
                listBoxProfiles.Items.Add(
                    Path.GetFileNameWithoutExtension(file)
                );
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        // ====================================================
        // Mod Profiles
        //
        // Save and load collections of enabled mods.
        // ====================================================

        private void buttonSaveProfile_Click(object sender, EventArgs e)
        {
            string profileName =
                Microsoft.VisualBasic.Interaction.InputBox(
                    "Enter a profile name:",
                    "Save Profile"
                );

            if (string.IsNullOrWhiteSpace(profileName))
                return;

            string profileFile =
                Path.Combine(
                    ProfilesFolder,
                    profileName + ".ini"
                );

            if (File.Exists(profileFile))
            {
                DialogResult result =
                    MessageBox.Show(
                        "A profile with this name already exists.\n\nOverwrite it?",
                        "Overwrite Profile",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                if (result != DialogResult.Yes)
                {
                    return;
                }
            }

            List<string> lines = new List<string>();

            lines.Add("[Profile]");
            lines.Add($"Name={profileName}");
            lines.Add("");
            lines.Add("[Mods]");

            foreach (object item in checkedListBoxMods.CheckedItems)
            {
                string mod = item.ToString();

                if (mod.StartsWith("⭐ "))
                {
                    mod = mod.Substring(2);
                }

                lines.Add(mod);
            }

            File.WriteAllLines(profileFile, lines);

            LoadProfiles();

            MessageBox.Show("Profile saved.");

            listBoxProfiles.SelectedItem = profileName;

            labelCurrentProfile.Text =
                "Loaded Profile: " + profileName;
        }

        private void buttonLoadProfile_Click(object sender, EventArgs e)
        {
            if (listBoxProfiles.SelectedItem == null)
            {
                MessageBox.Show("Please select a profile.");
                return;
            }

            string profileFile =
                Path.Combine(
                    ProfilesFolder,
                    listBoxProfiles.SelectedItem.ToString() + ".ini"
                );

            if (!File.Exists(profileFile))
                return;

            // Untick everything first
            for (int i = 0; i < checkedListBoxMods.Items.Count; i++)
            {
                checkedListBoxMods.SetItemChecked(i, false);
            }

            bool readingMods = false;

            foreach (string line in File.ReadAllLines(profileFile))
            {
                if (line == "[Mods]")
                {
                    readingMods = true;
                    continue;
                }

                if (!readingMods)
                    continue;

                string profileMod = line.Trim();

                for (int i = 0; i < checkedListBoxMods.Items.Count; i++)
                {
                    string mod =
                        checkedListBoxMods.Items[i].ToString();

                    if (mod.StartsWith("⭐ "))
                    {
                        mod = mod.Substring(2);
                    }

                    if (mod.Equals(profileMod, StringComparison.OrdinalIgnoreCase))
                    {
                        checkedListBoxMods.SetItemChecked(i, true);
                    }
                    labelCurrentProfile.Text =
    "Loaded Profile: " +
    listBoxProfiles.SelectedItem.ToString();
                }
            }
        }

        private void buttonDeleteProfile_Click(object sender, EventArgs e)
        {
            if (listBoxProfiles.SelectedItem == null)
            {
                MessageBox.Show("Please select a profile.");
                return;
            }

            string profileFile =
                Path.Combine(
                    ProfilesFolder,
                    listBoxProfiles.SelectedItem.ToString() + ".ini"
                );

            if (MessageBox.Show(
                "Delete this profile?",
                "Delete Profile",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question)
                != DialogResult.Yes)
            {
                return;
            }

            if (File.Exists(profileFile))
            {
                File.Delete(profileFile);
            }

            LoadProfiles();

            labelCurrentProfile.Text =
    "Loaded Profile: None";
        }

        private void buttonNewProfile_Click(object sender, EventArgs e)
        {
            // Untick every mod
            for (int i = 0; i < checkedListBoxMods.Items.Count; i++)
            {
                checkedListBoxMods.SetItemChecked(i, false);
            }

            // Deselect any selected profile
            listBoxProfiles.ClearSelected();

            MessageBox.Show(
                "Ready to create a new profile.\n\nSelect the mods you want, then click Save."
            );
            labelCurrentProfile.Text =
    "Loaded Profile: None";
        }

        // ====================================================
        // Metadata Editor
        //
        // Allows the launcher metadata stored in mod.ini to be edited directly.
        // ====================================================

        private void buttonEditMetadata_Click(object sender, EventArgs e)
        {
            if (checkedListBoxMods.SelectedItem == null)
            {
                MessageBox.Show("Please select a mod first.");
                return;
            }

            textBoxAuthor.Enabled = true;
            textBoxVersion.Enabled = true;
            textBoxWebsite.Enabled = true;
            textBoxDescription.Enabled = true;

            textBoxAuthor.ReadOnly = false;
            textBoxVersion.ReadOnly = false;
            textBoxWebsite.ReadOnly = false;
            textBoxDescription.ReadOnly = false;

            textBoxAuthor.Focus();
        }

        private void buttonSaveMetadata_Click(object sender, EventArgs e)
        {
            if (checkedListBoxMods.SelectedItem == null)
            {
                MessageBox.Show("Please select a mod first.");
                return;
            }



            string selectedMod = GetSelectedMod();

            MessageBox.Show(selectedMod);

            string iniPath =
                Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "Mods",
                    selectedMod,
                    "mod.ini"
                );

            if (!File.Exists(iniPath))
                return;

            List<string> lines =
                File.ReadAllLines(iniPath).ToList();

            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].StartsWith("Author="))
                    lines[i] = "Author=" + textBoxAuthor.Text;

                else if (lines[i].StartsWith("Version="))
                    lines[i] = "Version=" + textBoxVersion.Text;

                else if (lines[i].StartsWith("Website="))
                    lines[i] = "Website=" + textBoxWebsite.Text;

                else if (lines[i].StartsWith("PreviewImage="))
                    lines[i] = "PreviewImage=" + previewImageFile;
                else if (lines[i].StartsWith("Description="))
                    lines[i] = "Description=" + textBoxDescription.Text;
            }

            File.WriteAllLines(iniPath, lines);

            textBoxAuthor.ReadOnly = true;
            textBoxVersion.ReadOnly = true;
            textBoxWebsite.ReadOnly = true;
            textBoxDescription.ReadOnly = true;


            LoadModDetails();

        }

        private void buttonBrowsePreview_Click(object sender, EventArgs e)
        {
            if (checkedListBoxMods.SelectedItem == null)
            {
                MessageBox.Show("Please select a mod first.");
                return;
            }

            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filter =
                "Images|*.png;*.jpg;*.jpeg;*.bmp";

            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            string selectedMod = GetSelectedMod();

            string modFolder =
                Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "Mods",
                    selectedMod
                );

            previewImageFile =
                Path.GetFileName(dialog.FileName);

            string destination =
                Path.Combine(
                    modFolder,
                    previewImageFile
                );

            File.Copy(
                dialog.FileName,
                destination,
                true
            );

            LoadPreviewImage(destination);

        }

        private void buttonClearPreview_Click(object sender, EventArgs e)
        {
            if (pictureBoxPreview.Image != null)
            {
                pictureBoxPreview.Image.Dispose();
                pictureBoxPreview.Image = null;
            }

            previewImageFile = "";
        }
    }
}
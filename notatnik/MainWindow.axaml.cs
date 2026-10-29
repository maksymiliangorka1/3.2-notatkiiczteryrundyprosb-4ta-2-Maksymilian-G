using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace notatnik;

public partial class MainWindow : Window
{
    private static readonly string FilePath = Directory.Exists("notatnik")
        ? Path.Combine("notatnik", "notatki.md")
        : "notatki.md";

    private readonly ObservableCollection<string> _notes = new();

    public MainWindow()
    {
        InitializeComponent();
        NotesListBox.ItemsSource = _notes;
        LoadNotes();
    }

    private void LoadNotes()
    {
        _notes.Clear();
        if (File.Exists(FilePath))
        {
            var content = File.ReadAllText(FilePath, Encoding.UTF8);
            var entries = Regex.Split(content, @"(?:\r?\n\r?\n)+(?=## )");
            foreach (var entry in entries)
            {
                var trimmed = entry.Trim();
                if (!string.IsNullOrWhiteSpace(trimmed))
                {
                    _notes.Add(trimmed);
                }
            }
        }
    }

    private void SaveButton_Click(object? sender, RoutedEventArgs e)
    {
        var title = (TitleTextBox.Text ?? string.Empty).Trim();
        var content = (ContentTextBox.Text ?? string.Empty).Trim();

        var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
        var newEntry = $"## {timestamp} — {title}\n\n{content}";

        var existing = File.Exists(FilePath) ? File.ReadAllText(FilePath, Encoding.UTF8) : string.Empty;
        var updated = string.IsNullOrWhiteSpace(existing)
            ? newEntry
            : newEntry + "\n\n" + existing.Trim();

        File.WriteAllText(FilePath, updated, Encoding.UTF8);

        _notes.Insert(0, newEntry);

        TitleTextBox.Text = string.Empty;
        ContentTextBox.Text = string.Empty;
    }
}
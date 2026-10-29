using System;
using System.Collections.ObjectModel;
using System.IO;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace notatnik;

public partial class MainWindow : Window
{
    private const string FilePath = "notatki.txt";
    private readonly ObservableCollection<string> _notes = new();

    public MainWindow()
    {
        InitializeComponent();
        NotesListBox.ItemsSource = _notes;
        LoadNotes();
    }

    private void LoadNotes()
    {
        if (File.Exists(FilePath))
        {
            var content = File.ReadAllText(FilePath);
            var items = content.Split(new[] { "---" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in items)
            {
                var trimmed = item.Trim();
                if (!string.IsNullOrWhiteSpace(trimmed))
                {
                    _notes.Add(trimmed);
                }
            }
        }
    }

    private void SaveButton_Click(object? sender, RoutedEventArgs e)
    {
        var title = TitleTextBox.Text ?? string.Empty;
        var content = ContentTextBox.Text ?? string.Empty;

        var noteEntry = $"Tytuł: {title}\nTreść: {content}\n---\n";
        File.AppendAllText(FilePath, noteEntry);

        _notes.Add($"Tytuł: {title}\nTreść: {content}");

        TitleTextBox.Text = string.Empty;
        ContentTextBox.Text = string.Empty;
    }
}
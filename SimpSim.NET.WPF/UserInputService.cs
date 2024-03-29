﻿using System.IO;
using Microsoft.Win32;

namespace SimpSim.NET.WPF;

public interface IUserInputService
{
    FileInfo GetOpenFileName();
    FileInfo GetSaveFileName();
}

internal class UserInputService : IUserInputService
{
    public FileInfo GetOpenFileName()
    {
        return GetFileFromDialog(new OpenFileDialog(), "Supported File Types|*.asm;*.prg|Assembly Files|*.asm|Program Files|*.prg");
    }

    public FileInfo GetSaveFileName()
    {
        return GetFileFromDialog(new SaveFileDialog(), "Program Files|*.prg");
    }

    private FileInfo GetFileFromDialog(FileDialog fileDialog, string filter)
    {
        fileDialog.Filter = filter;
        fileDialog.ShowDialog();

        if (string.IsNullOrWhiteSpace(fileDialog.FileName))
            return null;
        else
            return new FileInfo(fileDialog.FileName);
    }
}
using HarmonyLib;
using Verse;

namespace ShowModUpdates;

[HarmonyPatch(typeof(SavedGameLoaderNow), nameof(SavedGameLoaderNow.LoadGameFromSaveFileNow))]
public static class SavedGameLoaderNow_LoadGameFromSaveFileNow_Postfix
{
    public static void Postfix(string fileName)
    {
        ShowModUpdates.FinishedLoading = false;
        if (Current.Game == null)
        {
            ShowModUpdates.CurrentSavePath = null;
            return;
        }

        ShowModUpdates.CurrentSavePath = GenFilePaths.FilePathForSavedGame(fileName);

        LongEventHandler.QueueLongEvent(ShowModUpdates.CheckModUpdates, "ShowModUpdates.CheckModUpdates.Save", true,
            null);
    }
}
// This code is part of the Fungus library (http://fungusgames.com) maintained by Chris Gregan (http://twitter.com/gofungus).
// It is released for free under the MIT open source license (https://github.com/snozbot/fungus/blob/master/LICENSE)

namespace GameMain.Dialog
{
    /// <summary>
    /// Writer event signaling system.
    /// You can use this to be notified about various events in the writing process.
    /// </summary>
    public static class WriterSignals
    {
        #region Public members

        /// <summary>
        /// TextTagToken signal. Sent for each unique token when writing text.
        /// </summary>
        public static event TextTagTokenHandler OnTextTagToken;
        public delegate void TextTagTokenHandler(DialogWrite writer, TextTagToken token, int index, int maxIndex);
        public static void DoTextTagToken(DialogWrite writer, TextTagToken token, int index, int maxIndex) { if(OnTextTagToken != null) OnTextTagToken(writer, token, index, maxIndex); }

        /// <summary>
        /// WriterState signal. Sent when the writer changes state.
        /// </summary>
        public static event WriterStateHandler OnWriterState;
        public delegate void WriterStateHandler(DialogWrite writer, DialogWriteState writerState);
        public static void DoWriterState(DialogWrite writer, DialogWriteState writerState) { if (OnWriterState != null) OnWriterState(writer, writerState); }

        /// <summary>
        /// WriterInput signal. Sent when the writer receives player input.
        /// </summary>
        public static event WriterInputHandler OnWriterInput;
        public delegate void WriterInputHandler(DialogWrite writer);
        public static void DoWriterInput(DialogWrite writer) { if (OnWriterInput != null) OnWriterInput(writer); }

        /// <summary>
        /// WriterGlyph signal. Sent when the writer writes out a glyph.
        /// </summary>
        public delegate void WriterGlyphHandler(DialogWrite writer);
        public static event WriterGlyphHandler OnWriterGlyph;
        public static void DoWriterGlyph(DialogWrite writer) { if (OnWriterGlyph != null) OnWriterGlyph(writer); }

        #endregion
    }
}

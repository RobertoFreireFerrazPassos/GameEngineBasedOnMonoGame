using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using System.IO;
using System;

namespace GameEngine.Elements.Managers;

public enum Note
{
    C, D, E, F, G, A, B
}

public static class MusicManager
{
    public static Dictionary<string,Melody> Melodies = new Dictionary<string, Melody>();

    public static void AddMelody(string melodyKey, (Note, uint)[] melody)
    {
        Melodies.Add(melodyKey, new Melody(melody));
    }

    public static void Play(string melodyKey)
    {
        Melodies[melodyKey].Play();
    }
}

public class Melody
{
    private SoundEffect sound;
    private MemoryStream audioStream;

    // Frequencies for musical notes
    private static readonly Dictionary<Note, double> NoteFrequencies = new Dictionary<Note, double>
    {
        { Note.C, 261.63 },
        { Note.D, 293.66 },
        { Note.E, 329.63 },
        { Note.F, 349.23 },
        { Note.G, 392.00 },
        { Note.A, 440.00 },
        { Note.B, 493.88 }
    };

    /// <summary>
    /// Generates a sound and saves it to a memory stream
    /// </summary>
    /// <param name="freq"></param>
    /// <param name="durationTenthSeconds"></param>
    public Melody((Note, uint)[] melody)
    {
        // Create memory stream and write data
        audioStream = new MemoryStream();
        BinaryWriter writer = new BinaryWriter(audioStream);

        // Constants for WAV file format
        const string headerGroupID = "RIFF";
        const string headerRiffType = "WAVE";
        const string fmtChunkID = "fmt ";
        const uint fmtChunkSize = 16;
        const ushort fmtFormatTag = 1; // PCM
        const ushort fmtChannels = 2;  // 1 - Mono 2 - stereo
        const uint fmtSamplesPerSec = 44100; // sample rate, e.g. CD=44100
        const ushort fmtBitsPerSample = 16;  // bits per sample
        const ushort fmtBlockAlign = (ushort)(fmtChannels * (fmtBitsPerSample / 8)); // sample frame size, in bytes
        const uint fmtAvgBytesPerSec = fmtSamplesPerSec * fmtBlockAlign;
        const string dataChunkID = "data";

        uint totalDuration = 0;
        foreach (var note in melody)
        {
            totalDuration += note.Item2;
        }

        var totalNumSamples = fmtSamplesPerSec * totalDuration / 10;
        var completeDataByteArray = new byte[totalNumSamples * 2];
        var lengthCopied = 0;

        // Frequency in Hertz
        // Duration in multiples of 1/10 second
        foreach (var (note, durationTenthSeconds) in melody)
        {
            if (!NoteFrequencies.TryGetValue(note, out double freq))
            {
                throw new ArgumentException("Invalid note");
            }

            // Number of samples = sample rate * channels * bytes per sample * duration in seconds
            uint numSamples = fmtSamplesPerSec * fmtChannels * durationTenthSeconds / 10;
            byte[] dataByteArray = new byte[numSamples];

            // Generate sine wave data
            int amplitude = 127, offset = 128; // for 8-audio
            double period = (2.0 * Math.PI * freq) / (fmtSamplesPerSec * fmtChannels);
            double amp;

            for (uint i = 0; i < numSamples - 1; i+= fmtChannels)
            {
                amp = amplitude * (double)(numSamples - i) / numSamples;
                // Fill with a waveform on each channel with amplitude decay
                for (int channel = 0; channel < fmtChannels; channel++)
                {
                    dataByteArray[i + channel] = Convert.ToByte(amp * Math.Sin(i * period) + offset);
                }
            }
            
            for (int i= lengthCopied; i <= lengthCopied + dataByteArray.Length - 1; i++)
            {
                completeDataByteArray[i] = dataByteArray[i - lengthCopied];
            }
            lengthCopied += dataByteArray.Length;
        }

        // Calculate chunk sizes
        uint dataChunkSize = (uint)completeDataByteArray.Length * (fmtBitsPerSample / 8);
        uint headerFileLength = 4 + (8 + fmtChunkSize) + (8 + dataChunkSize);

        writer.Write(headerGroupID.ToCharArray());
        writer.Write(headerFileLength);
        writer.Write(headerRiffType.ToCharArray());

        writer.Write(fmtChunkID.ToCharArray());
        writer.Write(fmtChunkSize);
        writer.Write(fmtFormatTag);
        writer.Write(fmtChannels);
        writer.Write(fmtSamplesPerSec);
        writer.Write(fmtAvgBytesPerSec);
        writer.Write(fmtBlockAlign);
        writer.Write(fmtBitsPerSample);

        writer.Write(dataChunkID.ToCharArray());
        writer.Write(dataChunkSize);
        writer.Write(completeDataByteArray);

        audioStream.Seek(0, SeekOrigin.Begin);
        sound = SoundEffect.FromStream(audioStream);
    }

    public void Play()
    {
        sound.Play();
    }
}

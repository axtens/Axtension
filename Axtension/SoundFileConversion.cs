using System.IO;
using NAudio;
using NAudio.Wave;
using NAudio.Lame;
using NAudio.FileFormats;
using NAudio.Codecs;
using NAudio.CoreAudioApi;

namespace Axtension
{
    public static class SoundFileConversion
    {

        // Convert WAV to MP3 using libmp3lame library
        public static void WaveToMP3(string waveFileName, string mp3FileName, int bitRate = 128)
        {
            using (var reader = new AudioFileReader(waveFileName))
            using (var writer = new LameMP3FileWriter(mp3FileName, reader.WaveFormat, bitRate))
                reader.CopyTo(writer);
        }

        // Convert MP3 file to WAV using NAudio classes only
        public static void MP3ToWave(string mp3FileName, string waveFileName)
        {
            using (var reader = new Mp3FileReader(mp3FileName))
            using (var writer = new WaveFileWriter(waveFileName, reader.WaveFormat))
                reader.CopyTo(writer);
        }

        // Convert M4A to MP3 using libmp3lame library
        public static void M4AToMP3(string m4aFileName, string mp3FileName, int bitRate = 128)
        {
            using (var reader = new MediaFoundationReader(m4aFileName))
            using (var writer = new LameMP3FileWriter(mp3FileName, reader.WaveFormat, bitRate))
                reader.CopyTo(writer);
        }

        // Convert MP3 file to M4A using NAudio classes only
        public static void MP3ToM4A(string mp3FileName, string m4aFileName)
        {
            using (var reader = new Mp3FileReader(mp3FileName))
                MediaFoundationEncoder.EncodeToAac(reader, m4aFileName);
        }
    }
}
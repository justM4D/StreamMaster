﻿using System.Text.Json.Serialization;

namespace StreamMaster.Streams.Domain.Models;

// Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
public class Disposition
{
    [JsonPropertyName("default")]
    public int Default { get; set; }

    [JsonPropertyName("dub")]
    public int Dub { get; set; }

    [JsonPropertyName("original")]
    public int Original { get; set; }

    [JsonPropertyName("comment")]
    public int Comment { get; set; }

    [JsonPropertyName("lyrics")]
    public int Lyrics { get; set; }

    [JsonPropertyName("karaoke")]
    public int Karaoke { get; set; }

    [JsonPropertyName("forced")]
    public int Forced { get; set; }

    [JsonPropertyName("hearing_impaired")]
    public int HearingImpaired { get; set; }

    [JsonPropertyName("visual_impaired")]
    public int VisualImpaired { get; set; }

    [JsonPropertyName("clean_effects")]
    public int CleanEffects { get; set; }

    [JsonPropertyName("attached_pic")]
    public int AttachedPic { get; set; }

    [JsonPropertyName("timed_thumbnails")]
    public int TimedThumbnails { get; set; }
}

public class Format
{
    [JsonPropertyName("filename")]
    public string Filename { get; set; }

    [JsonPropertyName("nb_streams")]
    public int NbStreams { get; set; }

    [JsonPropertyName("nb_programs")]
    public int NbPrograms { get; set; }

    [JsonPropertyName("format_name")]
    public string FormatName { get; set; }

    [JsonPropertyName("format_long_name")]
    public string FormatLongName { get; set; }

    [JsonPropertyName("start_time")]
    public string StartTime { get; set; }

    [JsonPropertyName("probe_score")]
    public int ProbeScore { get; set; }
}

public class VideoInfo
{
    [JsonPropertyName("streams")]
    public List<VideoStreamInfo> Streams { get; set; }

    [JsonPropertyName("format")]
    public Format Format { get; set; }
}

public class VideoStreamInfo
{
    [JsonPropertyName("index")]
    public int Index { get; set; }

    [JsonPropertyName("codec_name")]
    public string CodecName { get; set; }

    [JsonPropertyName("codec_long_name")]
    public string CodecLongName { get; set; }

    [JsonPropertyName("profile")]
    public string Profile { get; set; }

    [JsonPropertyName("codec_type")]
    public string CodecType { get; set; }

    [JsonPropertyName("codec_tag_string")]
    public string CodecTagString { get; set; }

    [JsonPropertyName("codec_tag")]
    public string CodecTag { get; set; }

    [JsonPropertyName("width")]
    public int Width { get; set; }

    [JsonPropertyName("height")]
    public int Height { get; set; }

    [JsonPropertyName("coded_width")]
    public int CodedWidth { get; set; }

    [JsonPropertyName("coded_height")]
    public int CodedHeight { get; set; }

    [JsonPropertyName("closed_captions")]
    public int ClosedCaptions { get; set; }

    [JsonPropertyName("has_b_frames")]
    public int HasBFrames { get; set; }

    [JsonPropertyName("sample_aspect_ratio")]
    public string SampleAspectRatio { get; set; }

    [JsonPropertyName("display_aspect_ratio")]
    public string DisplayAspectRatio { get; set; }

    [JsonPropertyName("pix_fmt")]
    public string PixFmt { get; set; }

    [JsonPropertyName("level")]
    public int Level { get; set; }

    [JsonPropertyName("color_range")]
    public string ColorRange { get; set; }

    [JsonPropertyName("color_space")]
    public string ColorSpace { get; set; }

    [JsonPropertyName("color_transfer")]
    public string ColorTransfer { get; set; }

    [JsonPropertyName("color_primaries")]
    public string ColorPrimaries { get; set; }

    [JsonPropertyName("chroma_location")]
    public string ChromaLocation { get; set; }

    [JsonPropertyName("field_order")]
    public string FieldOrder { get; set; }

    [JsonPropertyName("refs")]
    public int Refs { get; set; }

    [JsonPropertyName("is_avc")]
    public string IsAvc { get; set; }

    [JsonPropertyName("nal_length_size")]
    public string NalLengthSize { get; set; }

    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("r_frame_rate")]
    public string RFrameRate { get; set; }

    [JsonPropertyName("avg_frame_rate")]
    public string AvgFrameRate { get; set; }

    [JsonPropertyName("time_base")]
    public string TimeBase { get; set; }

    [JsonPropertyName("start_pts")]
    public object StartPts { get; set; }

    [JsonPropertyName("start_time")]
    public string StartTime { get; set; }

    [JsonPropertyName("bits_per_raw_sample")]
    public string BitsPerRawSample { get; set; }

    [JsonPropertyName("disposition")]
    public Disposition Disposition { get; set; }

    [JsonPropertyName("sample_fmt")]
    public string SampleFmt { get; set; }

    [JsonPropertyName("sample_rate")]
    public string SampleRate { get; set; }

    [JsonPropertyName("channels")]
    public int? Channels { get; set; }

    [JsonPropertyName("channel_layout")]
    public string ChannelLayout { get; set; }

    [JsonPropertyName("bits_per_sample")]
    public int? BitsPerSample { get; set; }

    [JsonPropertyName("bit_rate")]
    public string BitRate { get; set; }
}


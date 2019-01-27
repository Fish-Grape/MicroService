using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Feng.Files.Service
{
    public static class FileTypeEnum
    {
        private static Dictionary<string, string> FILE_TYPE = null;

        static FileTypeEnum()
        {
            FILE_TYPE = new Dictionary<string, string>();
            FILE_TYPE.Add("png", "image");
            FILE_TYPE.Add("jpg", "image");
            FILE_TYPE.Add("jpeg", "image");
            FILE_TYPE.Add("gif", "image");
            FILE_TYPE.Add("bmp", "image");
            FILE_TYPE.Add("mp4", "video");
            FILE_TYPE.Add("ogv", "video");
            FILE_TYPE.Add("webm", "video");
            FILE_TYPE.Add("flv", "video");
            FILE_TYPE.Add("avi", "video");
            FILE_TYPE.Add("mp3", "audio");
            FILE_TYPE.Add("wma", "audio");
            FILE_TYPE.Add("wav", "audio");
            FILE_TYPE.Add("pdf", "document");
            FILE_TYPE.Add("txt", "document");
            FILE_TYPE.Add("doc", "document");
            FILE_TYPE.Add("docx", "document");
            FILE_TYPE.Add("ppt", "document");
            FILE_TYPE.Add("xls", "document");
            FILE_TYPE.Add("xlsx", "document");
        }

        public static int GetFileTypeByV(string strType)
        {
            strType = strType.ToLower();

            if (!string.IsNullOrWhiteSpace(strType) && FILE_TYPE.Keys.Contains(strType))
            {
                switch (FILE_TYPE[strType])
                {
                    case "image":
                        return (int)FileType.IMAGE;
                    case "video":
                        return (int)FileType.VIDEO;
                    case "audio":
                        return (int)FileType.AUDIO;
                    case "document":
                        return (int)FileType.DOCUMENT;
                    default:
                        break;
                }
            }
            return (int)FileType.OTHER;
        }

        public static string GetFileTypeByK(int iType)
        {
            string strDesc = ((FileType)Enum.Parse(typeof(FileType), iType.ToString())).GetDescription();

            return strDesc.ToLower();
        }

        public static string GetDisposition(string strType)
        {
            // 判断使用浏览器打开还是附件模式
            if (ContentTypeDic.GetContentType(strType).StartsWith("image"))
            {
                return "inline";
            }
            return "attachment";
        }
    }

    public enum FileType
    {
        /// <summary>
        /// 图片
        /// </summary>
        [Description("Image")]
        IMAGE = 9001,
        /// <summary>
        /// 视频
        /// </summary>
        [Description("Video")]
        VIDEO = 9002,
        /// <summary>
        /// 文档
        /// </summary>
        [Description("Document")]
        DOCUMENT = 9003,
        /// <summary>
        /// 音频
        /// </summary>
        [Description("Audio")]
        AUDIO = 9004,
        /// <summary>
        /// 其它
        /// </summary>
        [Description("Other")]
        OTHER = 9005
    }
}

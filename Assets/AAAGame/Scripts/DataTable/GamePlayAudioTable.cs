//------------------------------------------------------------
//------------------------------------------------------------
// 此文件由工具自动生成，请勿直接修改。
// 生成时间：__DATA_TABLE_CREATE_TIME__
//------------------------------------------------------------

using GameFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityGameFramework.Runtime;

[System.Reflection.Obfuscation(Feature = "renaming", ApplyToMembers = false)]
/// <summary>
/// GamePlayAudioTable
/// </summary>
public class GamePlayAudioTable : DataRowBase
{
	private int m_Id = 0;
	/// <summary>
    /// 
    /// </summary>
    public override int Id
    {
        get { return m_Id; }
    }

        /// <summary>
        /// 请添加字段, 字段名首字母大写
        /// </summary>
        public string AudioPath
        {
            get;
            private set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string AudionGUID
        {
            get;
            private set;
        }

        /// <summary>
        /// 音效关键字
        /// </summary>
        public string AudioKeyword
        {
            get;
            private set;
        }

        /// <summary>
        /// 音效所在的组
        /// </summary>
        public Const.SoundGroup SoundGroup
        {
            get;
            private set;
        }

        /// <summary>
        /// 音效大小
        /// </summary>
        public float Volume
        {
            get;
            private set;
        }

        /// <summary>
        /// 音高
        /// </summary>
        public float Pitch
        {
            get;
            private set;
        }

        public override bool ParseDataRow(string dataRowString, object userData)
        {
            string[] columnStrings = dataRowString.Split(DataTableExtension.DataSplitSeparators);
            for (int i = 0; i < columnStrings.Length; i++)
            {
                columnStrings[i] = columnStrings[i].Trim(DataTableExtension.DataTrimSeparators);
            }

            int index = 0;
            index++;
            m_Id = int.Parse(columnStrings[index++]);
            index++;
            AudioPath = columnStrings[index++];
            AudionGUID = columnStrings[index++];
            AudioKeyword = columnStrings[index++];
            SoundGroup = DataTableExtension.ParseEnum<Const.SoundGroup>(columnStrings[index++]);
            Volume = float.Parse(columnStrings[index++]);
            Pitch = float.Parse(columnStrings[index++]);

            return true;
        }

        public override bool ParseDataRow(byte[] dataRowBytes, int startIndex, int length, object userData)
        {
            using (MemoryStream memoryStream = new MemoryStream(dataRowBytes, startIndex, length, false))
            {
                using (BinaryReader binaryReader = new BinaryReader(memoryStream, Encoding.UTF8))
                {
                    m_Id = binaryReader.Read7BitEncodedInt32();
                    AudioPath = binaryReader.ReadString();
                    AudionGUID = binaryReader.ReadString();
                    AudioKeyword = binaryReader.ReadString();
                    SoundGroup = binaryReader.ReadEnum<Const.SoundGroup>();
                    Volume = binaryReader.ReadSingle();
                    Pitch = binaryReader.ReadSingle();
                }
            }

            return true;
        }

//__DATA_TABLE_PROPERTY_ARRAY__
}

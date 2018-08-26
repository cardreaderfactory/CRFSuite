/********************************************************************************
 * \copyright
 * Copyright 2009-2017, Card Reader Factory.  All rights were reserved.
 * From 2018 this code has been made PUBLIC DOMAIN.
 * This means that there are no longer any ownership rights such as copyright, trademark, or patent over this code.
 * This code can be modified, distributed, or sold even without any attribution by anyone.
 *
 * We would however be very grateful to anyone using this code in their product if you could add the line below into your product's documentation:
 * Special thanks to Nicholas Alexander Michael Webber, Terry Botten & all the staff working for Operation (Police) Academy. Without these people this code would not have been made public and the existance of this very product would be very much in doubt.
 *
 *******************************************************************************/


using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using Serializable;
using crf.Algorithm;

namespace crf
{
    public class Card : ICard
    {
        private uint _swipes;
        private uint _cardNumber;
        private int _currentTimeFormat = -1;
        private string _timeAsString = "";
        public uint _timeStamp1;
        public uint _timeStamp2;


        /**
         * Array with 3 variants group, one for each track.
         */
        public Track[] _tracks;

        public double timeDiff = 0; /* from previous card */


        /** group color. 
         */
        int? _groupColor = null;

        #region Constructor      

        /**
         */
        public Card(uint swipes, uint cardNumber,
                    uint timeStamp1, uint timeStamp2,
                    byte[] cardBytes, int position,
                    ushort track1Len, ushort track2Len,
                    ushort track3Len, int otherDataLen)
        {

            _swipes = swipes;
            _cardNumber = cardNumber;
            _timeStamp1 = timeStamp1;
            _timeStamp2 = timeStamp2;

            //create a variant group for each track
            _tracks = new Track[3];
            if (track1Len > 0)
                _tracks[0] = new Track(cardBytes, position, track1Len, 0);
            else
                _tracks[0] = null;

            if (track2Len > 0)
                _tracks[1] = new Track(cardBytes, position + track1Len, track2Len, 1);
            else
                _tracks[1] = null;

            if (track3Len > 0)
                _tracks[2] = new Track(cardBytes, position + track1Len + track2Len, track3Len, 2);
            else
                _tracks[2] = null;

            position += track1Len + track2Len + track3Len + otherDataLen;

            deserialiseMetaData(cardBytes, ref position);

//            SetPreferredDirection();
        }

        #endregion

        #region Methods

        //private int GetPreferredDirection()
        //{
        //    int preferredDirection = 0;

        //    for (int i = 0; i < 3; i++)
        //    {
        //        if (_tracks[i] != null)
        //        {
        //            Variant variant = _tracks[i].GetPreferredVariant();

        //            if (variant != null)
        //            {
        //                switch (variant.direction)
        //                {
        //                    case 0:
        //                        preferredDirection--;
        //                        break;

        //                    case 1:
        //                        preferredDirection++;
        //                        break;
        //                }
        //            }
        //        }
        //    }

        //    if (preferredDirection > 0)
        //        return 1;
        //    else if (preferredDirection < 0)
        //        return 0;
        //    else
        //        return -1;
        //}

        //private void SetPreferredDirection()
        //{
        //    int preferredDirection = GetPreferredDirection();

        //    for (int i = 0; i < 3; i++)
        //    {
        //        if (_tracks[i] != null)
        //        {
        //            _tracks[i].PreferredDirection = preferredDirection;
        //        }
        //    }
        //}

        #endregion

        #region ICard interface functions

        /**
         * Returns a variant group with all variants for the specified track. 
         * 
         * @param trackNumber track number (0, 1, 2).
         * 
         * @return Variant group for the specified track number. Can be null if no tracks.
         */
        public Track GetTrack(int trackNumber)
        {
            if ((trackNumber == 0) || (trackNumber == 1) || (trackNumber == 2))
                return _tracks[trackNumber];

            return null;
        }

        public Variant GetVariant(int trackNumber)
        {
            Track track = GetTrack(trackNumber);

            if (track != null)
                return track.GetPreferredVariant();

            return null;
        }

        public string ReaderCard
        {
            get
            {
                return _swipes.ToString();
            }
        }

        public string CardNumber
        {
            get
            {
                return _cardNumber.ToString();
            }
        }
        public string Time
        {
            get
            {
                if (_currentTimeFormat == crf.Properties.Settings.Default.showTimeFormat && _timeAsString != "")
                    return _timeAsString;

                _timeAsString = "";
                DateTime time = new DateTime(1, 1, 1, 0, 0, 0);
                TimeSpan elapsedSpan;
                double timeDouble = TimeAsDouble;
                if (timeDouble < 0.0)
                    timeDouble = -timeDouble;
                /*
                 * 
                 *  0 = dd/mm/yyyy hh:mm:ss
                 *  1 = dd/mm/yyyy hh:mm:ss.fff
                 *  2 = hhhhhh:mm:ss
                 *  3 = hhhhhh:mm:ss.fff
                 *  4 = hh:mm:ss
                 *  5 = hh:mm:ss.fff
                 *  6 = ssssssssssssssss
                 *  7 = ssssssssssssssss.fff
                 *  8 = ssssss
                 *  9 = ssssss.fff
                 * 10 = diff
                 * 11 = diff.fff
                 * 
                 * */

                switch (crf.Properties.Settings.Default.showTimeFormat)
                {
                    case 0:
                    case 1:
                        time = new DateTime(1970, 1, 1, 0, 0, 0).ToLocalTime();
                        time = time.AddMilliseconds(timeDouble);
                        _timeAsString = String.Format("{0:dd/MM/yyyy HH:mm:ss}", time);
                        break;

                    case 2:
                    case 3:
                    case 4:
                    case 5:
                        time = new DateTime(1, 1, 1, 0, 0, 0);
                        time = time.AddMilliseconds(timeDouble);
                        elapsedSpan = new TimeSpan(time.Ticks);

                        if (crf.Properties.Settings.Default.showTimeFormat < 4)
                            _timeAsString = String.Format("{0:d6}:{1:mm:ss}", (int)elapsedSpan.TotalHours, time);
                        else
                            _timeAsString = String.Format("{0:d2}:{1:mm:ss}", (int)elapsedSpan.TotalHours, time);
                        break;

                        // how to pad with spaces:
                        // st = String.Format("{0,6:d}:{1:mm:ss}", (int)elapsedSpan.TotalHours, time);

                    case 6:
                    case 7:
                    case 8:
                    case 9:
                        time = new DateTime(1, 1, 1, 0, 0, 0);
                        time = time.AddMilliseconds(timeDouble);
                        elapsedSpan = new TimeSpan(time.Ticks);

                        if (crf.Properties.Settings.Default.showTimeFormat < 8)
                            _timeAsString = String.Format("{0:d16}", (int)elapsedSpan.TotalSeconds);
                        else
                            _timeAsString = String.Format("{0:d}", (int)elapsedSpan.TotalSeconds);                        
                        break;

                    case 10:
                    case 11:
                        time = new DateTime(1, 1, 1, 0, 0, 0);
                        if (timeDiff < 0.0)
                            timeDiff = -timeDiff;
                        time = time.AddMilliseconds(timeDiff);
                        elapsedSpan = new TimeSpan(time.Ticks);
                        _timeAsString = String.Format("{0,5:d}", (int)elapsedSpan.TotalSeconds);
                        break;
                }

                if (crf.Properties.Settings.Default.showTimeFormat % 2 != 0)
                    _timeAsString += string.Format(".{0:fff}", time);
                return _timeAsString;
            }
        }
        
        public int? TimeGroupColor
        {
            get
            {
                return _groupColor;
            }

            set
            {
                _groupColor = value;
            }
        }

        public double TimeAsDouble
        {
            get
            {
                //ensure that we use double arithmetic for this operation instead of integer.
                //these numbers can be really big.
                return ((double)_timeStamp1 * 1000) + _timeStamp2;
            }
        }

        public StringWithParity Track1
        {
            get { return GetTrackString(0); }

            set { SetTrackString(0, value); }
        }

        public StringWithParity Track2
        {
            get { return GetTrackString(1); }

            set { SetTrackString(1, value); }
        }

        public StringWithParity Track3
        {
            get { return GetTrackString(2); }

            set { SetTrackString(2, value); }
        }

        public StringWithParity GetTrackString(int track)
        {
            Variant variant = GetVariant(track);

            if (null != variant)
                return variant.toStringWithParity();
            else
                return StringWithParity.Empty;
        }

        public string TrackAsAlignedString(int track)
        {
            String st = "";

            Variant var1 = GetVariant(track);
            if (var1 != null && var1.Alignment != null && var1.Alignment.Value != 0)
                st = st.PadLeft(var1.Alignment.Value);
            
            switch (track)
            {
                case 0:
                    return st + Track1;
                case 1:
                    return st + Track2;
                case 2:
                    return st + Track3;
            }
            return st;            

        }

        public void SetTrackString(int track, StringWithParity value)
        {
            Track group = _tracks[track];

            if (group != null)
            {
                if (StringWithParity.IsNullOrEmpty(value))
                    _tracks[track] = null;
                else
                    group.SetTrack(value);
            }
            else if (!StringWithParity.IsNullOrEmpty(value))
            {
                //create an empty group and set the value.
                byte bpc = VariantSettings.bpc[track];
                byte add = VariantSettings.add[track];
                if (bpc < (byte)BpcSupported.Start)
                {
                    bpc = VariantSettings.bpc[track];
                    add = VariantSettings.add[track];
                }

                _tracks[track] = new Track(track, bpc, add, value/*, GetPreferredDirection()*/);
            }
        }

        public Variant Track1Variant
        {
            get { return GetVariant(0); }
        }

        public Variant Track2Variant
        {
            get { return GetVariant(1); }
        }

        public Variant Track3Variant
        {
            get { return GetVariant(2); }
        }

        public int Direction
        {
            get
            {
                for (int i = 0; i < 3; i++)
                {
                    Variant variant = GetVariant(i);

                    if (null != variant)
                        return variant.direction;
                }

                return -1;
            }
        }

        public Image DirectionAsImage
        {
            get
            {
                for (int i = 0; i < 3; i++)
                {
                    Variant variant = GetVariant(i);

                    if (null != variant)
                        return ResourcesLoader.directionToImage(variant.direction);
                }

                return ResourcesLoader.LoadImage(ResourcesLoader.ImageID.Empty);
            }
        }

        public string DirectionAsString
        {
            get
            {
                for (int i = 0; i < 3; i++)
                {
                    Variant variant = GetVariant(i);

                    if (null != variant)
                        if (variant.direction == 0)
                            return "<<--     ";
                        else
                            return "     -->>";
                }

                return string.Empty;
            }
        }

        public void merge(Card c)
        {
            for (int i = 0; i < _tracks.GetLength(0); i++)
                if (_tracks[i] != null && c._tracks[i] != null)
                    _tracks[i].merge(c._tracks[i]);
        }

        public static string ColumnName(int column)
        {
            string[] columnNames = { "Reader", "Card", "Direction","Time", "Track1", "Track2", "Track3"  };

            return columnNames[column];
        }

        public bool Empty(int minimumChars)
        {
            return TrackEmpty(0, minimumChars) && TrackEmpty(1, minimumChars) && TrackEmpty(2, minimumChars);
        }

        public bool TrackEmpty(int track, int minimumChars)
        {
            return ((_tracks[track] == null) || (_tracks[track]).IsEmpty(minimumChars));
        }

        public bool Contains(string text)
        {
            if (ReaderCard.IndexOf(text, StringComparison.CurrentCultureIgnoreCase) >= 0 ||
                CardNumber.IndexOf(text, StringComparison.CurrentCultureIgnoreCase) >= 0 ||
                Time.IndexOf(text, StringComparison.CurrentCultureIgnoreCase) >= 0)
                return true;

            for (int i = 0; i < 3; i++)
            {
                if ((_tracks[i] != null) && _tracks[i].Contains(text))
                    return true;
            }

            return false;
        }

        #endregion

        #region Functions needed to serialize object

        public static explicit operator a(Card c)
        {
            b[] bs = null;

            if (c._tracks != null)
                bs = Array.ConvertAll<Track, b>(c._tracks, delegate(Track group)
                {
                    if (group == null)
                        return null;
                    return (b)group;
                });

            return new a(c._swipes, c._cardNumber,
                         c._timeStamp1, c._timeStamp2,                         
                         bs);
        }

        /**
         * protected construtor that must be only used to build serialized cards
         */
        protected Card(a serializedA)
        {
            _swipes = serializedA._s;
            _cardNumber = serializedA._cN;
            _timeStamp1 = serializedA._tS1;
            _timeStamp2 = serializedA._tS2;

            _tracks = null;
            if (serializedA._t.Length > 0)
            {
                _tracks = new Track[serializedA._t.Length];
                for (int i = 0; i < _tracks.Length; i++)
                {
                    if (serializedA._t[i] != null)
                        _tracks[i] = Track.BuildFromB(i, serializedA._t[i]);
                    else
                        _tracks[i] = null;
                }
            }

//            SetPreferredDirection();
        }

        /**
         * Better to have this function here instead of in a class as a explicit cast. 
         * This way it will be obfuscated.
         */
        public static Card BuildFromA(a serializedA)
        {
            return new Card(serializedA);
        }

        #endregion

        public void deserialiseMetaData(byte[] buf, ref int index)
        {
            /* 7 is the minimum byte count required for the metadata: signature + 1 byte for length of each track */

            if (index + 7 > buf.Length)
                return;

            if ((buf[index] != (byte)'M') || (buf[index + 1] != (byte)'T') || (buf[index + 2] != (byte)'D'))
                return;
            if (buf[index + 3] != 0)
                return; /* only version 0 of metadata is supported */


            byte[] t = new byte[3];

            t[0] = buf[index + 4];
            t[1] = buf[index + 5];
            t[2] = buf[index + 6];


            if (index + 7 + t[0] + t[1] + t[2] > buf.Length)
                return;

            index += 7;
            /* metadata version 0 */
            for (int i = 0; i < 3; i++)
            {
                if (_tracks[i] != null && t[i] == _tracks[i].serialiseMetaDataSize())
                    _tracks[i].deserialiseMetaData(buf, ref index);
            }

        }

    }
}

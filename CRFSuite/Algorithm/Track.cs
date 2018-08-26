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

using crf.Algorithm;
using Serializable;
using System;
using System.Collections;
using System.Collections.Generic;

namespace crf
{
    public enum BpcSupported
    {
        Start = 4,
        End = 9,
        Range = End - Start
    };

    public class Track : IList, IDataGridViewProgressColumnValue
    {
        /**< all calculated variants */
        private List<Variant> variants = new List<Variant>();

        /**< track id; can be 0, 1 or 2 */
        private int trackId;

        /**< bpc used to create this variant group */
        private byte bpc;

        /**< add byte used */
        private byte add;

        /**< index of the preferred variant (variant with the biggest rate) */
        private int preferredVariantIndex;

        /**< index of the user preferred variant. -1 if none */
        private int preferredUserVariantIndex;

        /* Track bytes with a 0 at the beginning an another at the end */
        public byte[] trackBytes;

        /* Current sort order */
        private Serializable.SortOrder sortOrder;

        /**
         * Creates a variant group that only has an user string.
         */
        public Track(int trackId, byte bpc, byte add, StringWithParity value)
        {
            this.trackId = trackId;
            this.bpc = bpc;
            this.add = add;
            init(null, 0, 0);

            SetTrack(value);
        }

        /**
         * Creates a variant group using the default bpc for this track.
         */
        public Track(byte[] variantBytes, int startPosition, int len, int trackId) :
            this(variantBytes, startPosition, len, trackId, VariantSettings.bpc[trackId], VariantSettings.add[trackId])
        {
        }

        public Track(byte[] variantBytes, int startPosition, int len, int trackId, byte bpc, byte add)
        {
            this.trackId = trackId;
            this.bpc = bpc;
            this.add = add;

            init(variantBytes, startPosition, len);
        }


        private void init(byte[] variantBytes, int startPosition, int len)
        {
            sortOrder = Serializable.SortOrder.None;
            preferredVariantIndex = -1;
            preferredUserVariantIndex = -1;
            trackBytes = null;

            variants.Clear();

            if (len > 0)
            {
                //copy track bytes adding a 0 add the beginning and another at the end
                trackBytes = new byte[len + 2];
                Array.Copy(variantBytes, startPosition, trackBytes, 1, len);

                // calculate for two directions
                variants.Add(new Variant(trackBytes, bpc, add) { track = trackId, direction = 0 });
                variants.Add(new Variant(BitSolution.Reverse(trackBytes), bpc, add) { track = trackId, direction = 1 });
                variants.Sort(new VariantComparer(SortOrder.Descending));

                preferredUserVariantIndex = 0;
            }
        }


        public void merge(Track t) // not tested.
        {
            bool []m = new bool[t.variants.Count]; 

            this.variants.Sort(new VariantComparer(SortOrder.Descending));
            t.variants.Sort(new VariantComparer(SortOrder.Descending));

            for (int i = 0; i < this.variants.Count; i++)   // loop through each variant
            {
                for (int j = 0; j < t.variants.Count; j++) // find the first variant in the same direction
                    if (this.variants[i].direction == t.variants[j].direction && m[j] == false)
                    {
                        if (this.variants[i].direction == 0)
                            this.variants[i].init(this.variants[i].BinaryString + " " + t.variants[j].BinaryString, BitSolution.Unchanged, BitSolution.Unchanged);
                        else
                            this.variants[i].init(t.variants[i].BinaryString + " " + this.variants[j].BinaryString, BitSolution.Unchanged, BitSolution.Unchanged);

                        m[j] = true; // and mark it in order not to use it again
                    }
            }

            this.variants.Sort(new VariantComparer(SortOrder.Descending));
            this.trackBytes = this.variants[0].ByteArray;

            preferredUserVariantIndex = 0;
        }

        private void CalculatePreferredVariant()
        {
            //calculate preferred variant; now it is the first one (index 0) so start at index 1
            variants.Sort(new VariantComparer(sortOrder));
            preferredVariantIndex = 0;
        }

        #region Functions to access the prefered variant object

        /**
         * returns true if the prefered variant hasSS
         */
        public bool HasSS
        {
            get
            {
                bool retValue = false;

                Variant variant = GetPreferredVariant();

                if (null != variant)
                    retValue = variant.HasSS;

                return retValue;
            }
        }

        /**
         * returns true if the prefered variant hasES
         */
        public bool HasES
        {
            get
            {
                bool retValue = false;

                Variant variant = GetPreferredVariant();

                if (null != variant)
                    retValue = variant.HasES;

                return retValue;
            }
        }

        #endregion

        #region ITrack interface functions

        public bool IsEmpty(int minimumChars)
        {
            //a group is empty if there is no preferred variant.
            //track bytes can be null but user can add a string so 
            //_trackBytes == null is not a valid method to determine if a group is empty.
            if ((preferredVariantIndex == -1) && (preferredUserVariantIndex == -1))
                return true;

            Variant preferredVariant = GetPreferredVariant();
            if (preferredVariant != null)
            {
                if (preferredVariant.ToString().Length <= minimumChars)
                    return true;
                else
                    return false;
            }
            else
                return true;
        }

        public Variant GetPreferredVariant()
        {
            int variantIndex = GetPreferredVariantIndex();
            if (variantIndex >= 0 && variants != null && variants.Count > variantIndex)
                return variants[variantIndex];
            return null;
        }

        public int GetPreferredVariantIndex()
        {
            if (null != variants)
            {
                if (preferredUserVariantIndex >= 0)
                    return preferredUserVariantIndex;
                else
                    return preferredVariantIndex;
            }
            else
            {
                return -1;
            }
        }

        public Variant SetPreferredUserVariant(int index)
        {
            preferredUserVariantIndex = index;

            return GetPreferredVariant();
        }

        public void Sort(Serializable.SortOrder sortOrder)
        {
            if (this.sortOrder != sortOrder)
            {
                if (variants != null)
                {
                    Variant preferredVariant = GetPreferredVariant();

                    variants.Sort(new VariantComparer(sortOrder));

                    
                    //update preferred variant
                    if (preferredUserVariantIndex >= 0)
                        preferredUserVariantIndex = variants.IndexOf(preferredVariant);
                    else
                        preferredVariantIndex = variants.IndexOf(preferredVariant);

                    this.sortOrder = sortOrder;
                }
            }
        }

        /** Set the preferred direction for this group */
        //public int PreferredDirection
        //{
        //    set
        //    {
        //        preferredDirection = value;
        //        CalculatePreferredVariant();
        //    }
        //}

        public Serializable.SortOrder CurrentSort()
        {
            return sortOrder;
        }

        public int BitsPerChar
        {
            get
            {
                return bpc;
            }
        }

        public byte AddByte
        {
            get
            {
                return add;
            }
        }

        public int TrackId
        {
            get
            {
                return trackId;
            }
        }

        public byte[] TrackBytes
        {
            get
            {
                if (trackBytes == null)
                    return null;

                byte[] ret = new byte[trackBytes.Length - 2];

                Array.Copy(trackBytes, 1, ret, 0, ret.Length);

                return ret;
            }
        }

        public void SetTrack(StringWithParity stringWP)
        {
            Variant preVariant = GetPreferredVariant();

            byte[] trackData = null;

            if (preVariant == null)
            {
                //int direction = (preferredDirection == 1) ? 1 : 0;
                preVariant = new Variant(stringWP, bpc, add);
            }

            trackData = preVariant.ByteArray;

            if (sortOrder != Serializable.SortOrder.None)
                Sort(sortOrder);

            for (int i = 0; i < variants.Count; i++)
            {
                if (stringWP.ToString() == variants[i].ToString())
                {
                    preferredVariantIndex = i;
                    break;
                }
            }

        }

        public void ChangeGroupSettings(byte bpc, byte add)
        {
            this.bpc = bpc;
            this.add = add;

            Serializable.SortOrder sortOrder = this.sortOrder;

            //remove 0 at the beginning and at the end.
            init(trackBytes, 1, trackBytes.Length - 2);

            if (sortOrder != Serializable.SortOrder.None)
                Sort(sortOrder);
        }

        #endregion

        #region IDataGridViewProgressColumnValue interface functions

        public int MaximumValue
        {
            get
            {
                if (null != trackBytes)
                    //two bytes from the _trackBytes are for the starting and ending 0
                    //four points more for good beginning and good ending.
                    return ((trackBytes.Length - 2) * 8 / bpc) + 4;
                else
                    return 0;
            }
        }

        #endregion

        #region IList interface

        #region IList interface functions

        public int Add(object value)
        {
            throw new NotSupportedException();
        }

        public void Clear()
        {
            throw new NotSupportedException();
        }

        public bool Contains(Object value)
        {
            return (IndexOf(value) != -1);
        }

        public bool Contains(string text)
        {
            if (variants != null)
            {
                foreach (Variant variant in variants)
                {
                    if (variant.ToString().IndexOf(text, StringComparison.CurrentCultureIgnoreCase) >= 0)
                        return true;
                }
            }

            return false;
        }

        public int IndexOf(Object value)
        {
            for (int i = 1; i < variants.Count; i++)
            {
                if (variants[i] == value)
                    return i;

            }

            return -1;
        }

        public void Insert(int index, Object value)
        {
            throw new NotSupportedException();
        }


        public void Remove(Object value)
        {
            throw new NotSupportedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotSupportedException();
        }

        #endregion

        #region IList interface properties

        public bool IsFixedSize
        {
            get
            {
                return true;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public Object this[int index]
        {
            get
            {
                return variants[index];
            }

            set
            {
                throw new NotSupportedException();
            }
        }


        #endregion

        #region IEnumerable interface

        public IEnumerator GetEnumerator()
        {
            return variants.GetEnumerator();
        }

        #endregion

        #region ICollection interface

        public int Count
        {
            get
            {
                return variants.Count;
            }
        }

        public bool IsSynchronized
        {
            get
            {
                return false;
            }
        }

        public object SyncRoot
        {
            get
            {
                return null;
            }
        }

        public void CopyTo(Array array, int index)
        {
            if (null != variants)
                for (int i = 0; i < variants.Count; i++)
                    array.SetValue(variants[i], i + index);
        }

        #endregion

        #endregion

        #region Functions needed to serialize object

        public static explicit operator b(Track g)
        {
            List<c> cs = new List<c>();

            if (g != null && g.variants != null)
                g.variants.ConvertAll<c>(delegate(Variant variant)
                {
                    if (variant == null)
                        return null;

                    //return (c)variant;
                    return null;
                });

            return new b(cs, g.bpc, g.add, g.preferredVariantIndex, g.preferredUserVariantIndex, g.trackBytes, g.sortOrder);
        }

        /**
         * protected constructor that must be only used to build serialized cards
         */
        protected Track(int trackId, b serializedB)
        {
            this.trackId = trackId;
            bpc = serializedB._bpc;
            add = serializedB._add;
            preferredVariantIndex = serializedB._pVI;
            preferredUserVariantIndex = serializedB._pUVI;
            trackBytes = serializedB._tB;
            sortOrder = Serializable.SortOrder.None;

            variants = null;
            if (serializedB._aV.Count > 0)
            {
//                variants = new Variant[serializedB._aV.Length];
                variants.Clear();
                for (int i = 0; i < variants.Count; i++)
                {
                    if (trackBytes != null)
                    {
                        int direction = 1;
                        if (i < (variants.Count / 2))
                            direction = 0;
                        variants[i] = new Variant(trackBytes, bpc, add) { direction = direction, track = trackId };
                    }
                    else
                    {
                        variants[i] = null;
                    }
                }

                if (serializedB._sO != Serializable.SortOrder.None)
                {
                    Sort(serializedB._sO);

                    //this must be done after sorting again!!
                    preferredVariantIndex = serializedB._pVI;
                    preferredUserVariantIndex = serializedB._pUVI;
                }

                //set user strings when sort order is correct
                for (int i = 0; i < variants.Count; i++)
                {
                    if ((serializedB._aV[i] != null) && (serializedB._aV[i]._uS != null))
                    {
                        SetTrack(new StringWithParity(serializedB._aV[i]._uS));
                        break;//there was only one user string.
                    }
                }
            }
        }

        /**
         * Better to have this function here instead of in b class as a explicit cast. 
         * This way it will be obfuscated.
         */
        public static Track BuildFromB(int trackId, b serializedB)
        {
            return new Track(trackId, serializedB);
        }

        public int serialiseMetaDataSize()
        {
            /* returns the size in bytes of the metadata; has to be the exact number of bytes written by serialiseMetaData() */
            return 5;
        }

        public void serialiseMetaData(ref byte[] buf, ref int index)
        {
            int startIndex = index;
            if (index + buf.Length < serialiseMetaDataSize())
                throw new Exception("buffer to small");

            buf[index++] = (byte)bpc;
            buf[index++] = (byte)add;
//            buf[index++] = (byte)preferredDirection;
            buf[index++] = (byte)preferredVariantIndex;
            buf[index++] = (byte)preferredUserVariantIndex;
            buf[index++] = (byte)sortOrder;

#if DEBUG
            if (index - startIndex != serialiseMetaDataSize())
                throw new Exception("serialiseSize is buggy (returning the wrong buffer size)");
#endif
        }

        public void deserialiseMetaData(byte[] buf, ref int index)
        {
            byte bpc;
            byte add;
            int startIndex = index;
            int preferredVariantIndex;
            int preferredUserVariantIndex;
            Serializable.SortOrder sortOrder;

            if (index + buf.Length < serialiseMetaDataSize())
                throw new Exception("buffer to small");

            /* read the settings */
            bpc = (byte)buf[index++];
            add = buf[index++];
//            preferredDirection = (int)buf[index++];
            preferredVariantIndex = (int)buf[index++];
            preferredUserVariantIndex = (int)buf[index++];
            sortOrder = (Serializable.SortOrder)buf[index++];

            /* correct the int to byte conversion issue */
            if ((preferredVariantIndex) == 0xff) preferredVariantIndex = -1;
            if ((preferredUserVariantIndex) == 0xff) preferredUserVariantIndex = -1;
//            if ((preferredDirection) == 0xff) preferredDirection = -1;

            /* apply settings */
            this.preferredVariantIndex = preferredVariantIndex;
            this.preferredUserVariantIndex = preferredUserVariantIndex;

            /* change bpc if required */
            if (this.bpc != bpc || this.add != add)
                ChangeGroupSettings(bpc, add);

            Sort(sortOrder);
            /* we have to apply this again according to Francis */
            this.preferredVariantIndex = preferredVariantIndex;
            this.preferredUserVariantIndex = preferredUserVariantIndex;


#if DEBUG
            if (index - startIndex != serialiseMetaDataSize())
                throw new Exception("serialiseSize is buggy (returning the wrong buffer size)");
#endif
        }

        #endregion
    }
}

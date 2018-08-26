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
using System.IO;
using System.Reflection;

namespace crf
{
    class ResourcesLoader
    {
        /**
         * ID for all possible images
         */
        public enum ImageID
        {
            Empty = 0,
            DirectionLeftToRight = 1,
            DirectionRightToLeft = 2
        }

        /**
         * Array with all images loaded
         */
        private static Image[] images = {null, null, null};

        private static string[] imageNames = {
                            "",   //empty image
                            "DirectionIn.png",
                            "DirectionOut.png"
                                             };

        static public Image directionToImage(int direction)
        {
            if (direction == 0)
                return ResourcesLoader.LoadImage(ResourcesLoader.ImageID.DirectionLeftToRight);
            else if (direction == 1)
                return ResourcesLoader.LoadImage(ResourcesLoader.ImageID.DirectionRightToLeft);
            else
                return ResourcesLoader.LoadImage(ResourcesLoader.ImageID.Empty);
        }


        /**
         * Returns the image for the specified id. If the ID does not exist returns null.
         * 
         * @param id ID of the image to return.
         * 
         * @return Image for that id.
         */
        public static Image LoadImage(ImageID id)
        {
            //check that id is correct
            if ((int)id >= images.Length)
                return null;

            //image already loaded?
            if (images[(int)id] != null)
                return images[(int)id];

            //load image
            switch (id)
            {
                case ImageID.Empty:
                    images[(int)id] = new Bitmap(1, 1, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                    break;

                default:
                    //check that id is correct
                    if ((int)id >= imageNames.Length)
                        return null;

                    Assembly assembly = Assembly.GetExecutingAssembly();
                    Stream imageStream = assembly.GetManifestResourceStream("crf.Resources." + imageNames[(int)id]);

                    Bitmap bitmap = new Bitmap(imageStream);
                    if (null != bitmap)
                        bitmap.MakeTransparent(Color.White);
                    images[(int)id] = bitmap;
                    break;
            }

            return images[(int)id];
        }
    }
}

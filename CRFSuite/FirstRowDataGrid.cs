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
using crf.Algorithm;

namespace crf
{
    public class FirstRowDataGrid : ICard
    {
        private static Image emptyImage = ResourcesLoader.LoadImage(ResourcesLoader.ImageID.Empty);
        
        private static string columnsString = "         1         2         3         4         5         6         7         8         9        10        11        12\n123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890";

        public string ReaderCard { get { return string.Empty; } }

        public string CardNumber { get { return string.Empty; } }

        public string Time { get { return string.Empty; } set { } }

        public StringWithParity Track1 
        { 
            get 
            {
                return new StringWithParity(columnsString); 
            } 
            
            set { ; } 
        }

        public StringWithParity Track2 
        { 
            get 
            {
                return new StringWithParity(columnsString);
            } 
            
            set { ; } 
        }

        public StringWithParity Track3 
        { 
            get 
            {
                return new StringWithParity(columnsString);
            } 
            
            set { ; } 
        }

        public Image DirectionAsImage { get { return emptyImage; } }
    }
}

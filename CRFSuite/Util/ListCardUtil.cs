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
using System.ComponentModel;
using crf.Algorithm;

namespace crf
{
    public class ListCardUtil
    {
        public static void SetGroupCards(IList<ICard> cards, int startIndex)
        {
            //can be only 0 or 1
            int colorIndex = startIndex;
            bool previousSet = false;

            if (cards.Count > startIndex)
            {
                if (cards.Count == (startIndex + 1))
                {
                    ((Card)cards[startIndex]).TimeGroupColor = null;
                }
                else
                {
                    for (int i = startIndex; i < cards.Count - 1; i++)
                    {
                        Card card1 = (Card)cards[i];
                        Card card2 = (Card)cards[i + 1];

                        //if cards are less than 200 ms they are in the same group.
                        if (Math.Abs(card2.TimeAsDouble - card1.TimeAsDouble) <= Forms.DecodeSettings.groupSwipesInterval)
                        {
                            card1.TimeGroupColor = colorIndex;
                            card2.TimeGroupColor = colorIndex;

                            previousSet = true;
                        }
                        else if (previousSet)
                        {
                            colorIndex++;
                            colorIndex %= 2;
                            previousSet = false;
                        }
                        else
                        {
                            colorIndex = 0;
                            card1.TimeGroupColor = null;
                            card2.TimeGroupColor = null;
                        }
                    }
                }
            }
        }

        public static void SetAlignment(IList<ICard> cards, int startIndex)
        {
            Card card1 = null;
            Card card2 = null;

            for (int i = startIndex; i < cards.Count; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    card1 = (Card)cards[i];
                    Variant var = card1.GetVariant(j);

                    if (var != null)
                        var.Alignment = null;
                }
            }

            //calculate alignment
            for (int i = startIndex; i < cards.Count - 1; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    int strIndex1, strIndex2;
                    string sequence;

                    card1 = (Card)cards[i];
                    card2 = (Card)cards[i + 1];

                    Variant var1 = card1.GetVariant(j);
                    Variant var2 = card2.GetVariant(j);

                    if ((var2 == null) || (var1 == null))
                        continue;

                    //issue 5: A card with SS and ES should not be aligned by the previous card.
                    if (var2.HasSS && var2.HasES && (var1.Alignment != null))
                        continue;

                    if (StringUtil.LongestCommonSubstring(var1.ToString(),
                                                          var2.ToString(),Properties.Settings.Default.alignChars,
                                                          out strIndex1, out strIndex2, out sequence))
                    {
                        //issue 5: A card with SS and ES should not be aligned by the previous card.
                        if ((var1.Alignment == null) && (!(var1.HasSS && var1.HasES)))
                            var1.Alignment = 0;

                        //issue 5: A card with SS and ES should not be aligned by the previous card.
                        if (var2.HasSS && var2.HasES)
                        {
                            if (strIndex1 < strIndex2)
                                var1.Alignment = strIndex2 - strIndex1;
                        }
                        else
                        {
                            if (strIndex1 > strIndex2)
                                var2.Alignment = strIndex1 - strIndex2 + (var1.Alignment ?? 0);
                            else
                                var2.Alignment = (var1.Alignment ?? 0) - (strIndex2 - strIndex1);
                        }
                    }
                }
            }

            //adjust negative alignments
            for (int j = 0; j < 3; j++)
            {
                int firstCard = -1;
                int alignment = 0;

                for (int i = startIndex; i < cards.Count; i++)
                {
                    card1 = (Card)cards[i];

                    Variant var1 = card1.GetVariant(j);

                    if ((var1 != null) && (var1.Alignment != null))
                    {
                        if (firstCard == -1)
                            firstCard = i;

                        if (var1.Alignment.Value < alignment)
                            alignment = var1.Alignment.Value;
                    }
                    else
                    {
                        if ((firstCard >= 0) && (alignment < 0))
                            SetAlignment(cards, j, firstCard, i - 1, alignment);

                        firstCard = -1;
                        alignment = 0;
                    }
                }

                if ((firstCard >= 0) && (alignment < 0))
                    SetAlignment(cards, j, firstCard, cards.Count - 1, alignment);
            }
        }

        private static void SetAlignment(IList<ICard> cards, int track, int firstCard, int lastCard, int alignment)
        {
            for (; firstCard <= lastCard; firstCard++)
            {
                Card card1 = (Card)cards[firstCard];

                Variant var1 = card1.GetVariant(track);

                if (var1 != null)
                {
                    //issue 5: A card with SS and ES should not be aligned by the previous card.
                    if (!(var1.HasSS && var1.HasES))
                        var1.Alignment -= alignment;
                }
            }
        }

    }
}

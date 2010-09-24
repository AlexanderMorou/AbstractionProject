using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
 /*----------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.     |
 |-----------------------------------------|
 | The Abstraction Project's code is prov- |
 | -ided under a contract-release basis.   |
 | DO NOT DISTRIBUTE and do not use beyond |
 | the contract terms.                     |
 \--------------------------------------- */

namespace AllenCopeland.Abstraction.OwnerDrawnControls
{
    /// <summary>
    /// Provides a simple owner drawn style implementation.
    /// </summary>
    /// <typeparam name="TDrawnItem">The <see cref="IOwnerDrawnItem"/>
    /// represented by the current implementation which needs
    /// metric calculation/drawing functionality.</typeparam>
    public class OwnerDrawnSimpleStyle<TDrawnItem> : OwnerDrawnStyle<TDrawnItem>
        where TDrawnItem :
            IOwnerDrawnItem<TDrawnItem>
    {
        private System.Drawing.Color AdjustColor(System.Drawing.Color original, int r, int g, int b)
        {
            return Color.FromArgb(original.A, Math.Max(original.R + r, 0), Math.Max(original.G + g, 0), Math.Max(original.B + b, 0));
        }
        /// <summary>
        /// Informs the <see cref="OwnerDrawnSimpleStyle{TDrawnItem}"/> to draw a
        /// specific element as defined within the arguments <paramref name="e"/>.
        /// </summary>
        /// <param name="e">The <see cref="IOwnerDrawnItemDrawEventArgs{TDrawnItem}"/>
        /// which denote the specific item and other elements
        /// associated with the drawing operation to be performed.</param>
        public override void OnDrawItem(IOwnerDrawnItemDrawEventArgs<TDrawnItem> e)
        {
            #region Colors
            System.Drawing.Color TextAreaColor = SystemColors.Window;
            System.Drawing.Color TextAreaTopmost = SystemColors.Control;
            System.Drawing.Color TextColor = SystemColors.MenuText;
            System.Drawing.Color ImageAreaStartColor = SystemColors.Window;
            System.Drawing.Color ImageAreaEndColor = AdjustColor(SystemColors.ButtonFace, -20, -20, -20);
            System.Drawing.Color SelBorderColor = SystemColors.Highlight;
            System.Drawing.Color SelAreaBackColor = Color.FromArgb((Math.Min((int)SelBorderColor.R + 120, (int)255)), (Math.Min((int)SelBorderColor.G + 120, (int)255)), (Math.Min((int)SelBorderColor.B + 120, (int)255)));
            System.Drawing.Color ImageAreaCheckColor = Color.FromArgb((Math.Min((int)SelBorderColor.R + 80, (int)255)), (Math.Min((int)SelBorderColor.G + 80, (int)255)), (Math.Min((int)SelBorderColor.B + 80, (int)255)));
            System.Drawing.Color SelTextAreaTopmost = SystemColors.Control;
            System.Drawing.Color HighTextAreaTopmost = SelAreaBackColor;
            System.Drawing.Color SelAreaForeColor = TextColor;
            System.Drawing.Color DisabledText = SystemColors.GrayText;
            #endregion
            #region Brushes
            System.Drawing.Brush ImageBackAreaBrush = null;
            System.Drawing.Brush TextBackAreaBrush = null;
            System.Drawing.Brush SelectionAreaBrush = null;
            System.Drawing.Brush StringBrush = null;
            #endregion
            #region Declarations
            RectangleF textLocale;
            RectangleF backgroundLocale;
            String ItemText = "";
            String ItemShortcut = "";
            StringFormat ItemStrFormat = new StringFormat();
            ItemStrFormat.LineAlignment = StringAlignment.Center;
            ItemStrFormat.HotkeyPrefix = HotkeyPrefix.Show;
            Bitmap ItemImage = null;
            Bitmap ItemShadowImage = null;
            Bitmap ItemDisabledImage = null;
            #endregion

            #region Setup
            backgroundLocale = new RectangleF(e.Bounds.Left, e.Bounds.Top, e.Bounds.Width, e.Bounds.Height);
            RectangleF ImageArea = new RectangleF(new PointF(e.Bounds.X, e.Bounds.Y), new SizeF(24, e.Bounds.Height));
            textLocale = new RectangleF(ImageArea.Right, backgroundLocale.Top, backgroundLocale.Width - ImageArea.Width, backgroundLocale.Height);
            //If the item's enabled..
            bool ItemSeparator = false;
            if (e.Item.Text != "" && e.Item.Text != null)
                ItemSeparator = (e.Item.Text.Substring(0, 1) == "-");
            else
                ItemSeparator = false;
            bool ItemEnabled = (!((e.State & DrawItemState.Disabled) == DrawItemState.Disabled)) && (!ItemSeparator);
            bool ItemHighlighted = ((e.State & DrawItemState.HotLight) == DrawItemState.HotLight);
            bool ItemChecked = ((e.State & DrawItemState.Checked) == DrawItemState.Checked);
            bool ItemSelected = ((e.State & DrawItemState.Selected) == DrawItemState.Selected) && !(ItemSeparator);
            bool ItemFocused = ((e.State & DrawItemState.Focus) == DrawItemState.Focus);
            bool ItemTopmost = (e.TopLevel);
            IOwnerDrawnMenuItem<TDrawnItem> menuItem = null;
            if (e.Item is IOwnerDrawnMenuItem<TDrawnItem>)
                menuItem = (IOwnerDrawnMenuItem<TDrawnItem>)e.Item;
            if (menuItem != null)
                ItemFocused = true;
            if (menuItem != null && ItemTopmost)
            {
                ItemStrFormat.Alignment = StringAlignment.Center;
                if (ItemSelected)
                {
                    SelectionAreaBrush = new SolidBrush(SelTextAreaTopmost);
                    if (ItemEnabled)
                        StringBrush = new SolidBrush(SelAreaForeColor);
                    else
                        StringBrush = new SolidBrush(DisabledText);
                }
                else if (ItemHighlighted)
                {
                    if (ItemEnabled)
                        StringBrush = new SolidBrush(SelAreaForeColor);
                    else
                        StringBrush = new SolidBrush(DisabledText);
                    SelectionAreaBrush = new SolidBrush(HighTextAreaTopmost);
                }
                else
                {
                    TextBackAreaBrush = new SolidBrush(TextAreaTopmost);
                    StringBrush = new SolidBrush(TextColor);
                }
            }
            else
            {
                ItemStrFormat.Alignment = StringAlignment.Near;
                if (ItemSelected)
                {
                    if (ItemEnabled)
                    {
                        if (!ItemFocused)
                        {
                            int backColorStartMedium = (int)(((float)SelAreaBackColor.R + (float)SelAreaBackColor.G + (float)SelAreaBackColor.B) / 3);
                            int foreColorMedium = (int)(((float)SelAreaForeColor.R + (float)SelAreaForeColor.G + (float)SelAreaForeColor.B) / 3);
                            SelAreaBackColor = Color.FromArgb(backColorStartMedium, backColorStartMedium, backColorStartMedium);
                            if (!ItemHighlighted)
                                SelAreaForeColor = Color.FromArgb(foreColorMedium, foreColorMedium, foreColorMedium);
                            else
                                SelAreaForeColor = SystemColors.HighlightText;
                        }
                        StringBrush = new SolidBrush(SelAreaForeColor);
                        SelectionAreaBrush = new SolidBrush(SelAreaBackColor);//new System.Drawing.Drawing2D.LinearGradientBrush(e.Bounds, SelAreaBackColor, ImageAreaCheckColor, 0, true);
                    }
                    else
                    {
                        StringBrush = new SolidBrush(DisabledText);
                        SelectionAreaBrush = new SolidBrush(TextAreaColor);
                    }
                }
                else
                {
                    ImageBackAreaBrush = new System.Drawing.Drawing2D.LinearGradientBrush(ImageArea, ImageAreaStartColor, ImageAreaEndColor, 0, true);
                    TextBackAreaBrush = new System.Drawing.SolidBrush(TextAreaColor);//new System.Drawing.Drawing2D.LinearGradientBrush(e.Bounds, TextAreaColor, TextAreaEndColor, 0, true);
                    if (ItemEnabled)
                        StringBrush = new SolidBrush(TextColor);
                    else
                        StringBrush = new SolidBrush(DisabledText);
                }
            }
            #endregion
            if (menuItem != null && ItemTopmost)
            {
                RectangleF TextArea = new RectangleF(new PointF(e.Bounds.X, e.Bounds.Y), new SizeF(e.Bounds.Width, e.Bounds.Height));
                if (ItemSelected || ItemHighlighted)
                {
                    e.Graphics.FillRectangle(SelectionAreaBrush, TextArea);
                    if (ItemHighlighted)
                        e.Graphics.DrawRectangle(SystemPens.Highlight, e.Bounds.X, e.Bounds.Y, e.Bounds.Width - 1, e.Bounds.Height - 1);
                    else if (ItemSelected)
                        e.Graphics.DrawLines(SystemPens.ControlDark, new PointF[] { new PointF(e.Bounds.X, e.Bounds.Bottom - 1), new PointF(e.Bounds.X, e.Bounds.Top), new PointF(e.Bounds.Right - 1, e.Bounds.Top), new PointF(e.Bounds.Right - 1, e.Bounds.Bottom - 1) });
                }
                else
                {
                    e.Graphics.FillRectangle(TextBackAreaBrush, TextArea);
                }
                e.Graphics.DrawString(menuItem.Text, e.Font, StringBrush, e.Bounds, ItemStrFormat);
            }
            else
            {
                RectangleF TextArea = new RectangleF(new PointF(ImageArea.Right, e.Bounds.Y), new SizeF(e.Bounds.Right - ImageArea.Right, e.Bounds.Height));
                int tabChar;
                if (e.Item.Text != "")
                    tabChar = e.Item.Text.IndexOf("\t", 0);
                else
                    tabChar = -1;
                if (menuItem != null && tabChar == -1)
                {
                    ItemText = e.Item.Text;
                    ItemShortcut = GetShortcutText(menuItem.Shortcut);
                }
                else if (tabChar != -1)
                {
                    ItemText = e.Item.Text.Substring(0, tabChar);
                    ItemShortcut = e.Item.Text.Substring(tabChar + 1);
                }
                else
                    ItemText = e.Item.Text;
                if (e.Item is IOwnerDrawnCheckableItem<TDrawnItem>)
                {
                    IOwnerDrawnCheckableItem<TDrawnItem> icodi = (IOwnerDrawnCheckableItem<TDrawnItem>)e.Item;
                    if (ItemChecked)
                    {
                        if (ItemEnabled)
                        {
                            if (icodi.CheckedImage != null)
                            {
                                ItemImage = icodi.CheckedImage;
                                ItemShadowImage = icodi.CheckedShadowImage;
                            }
                            else if (icodi.CheckedImage == null && icodi.Image == null)
                            {
                                ItemImage = OwnerDrawnStyle.DefaultItemCheckedImage;
                                ItemShadowImage = OwnerDrawnStyle.DefaultItemCheckedShadowImage;
                            }
                            else
                            {
                                ItemImage = icodi.Image;
                                ItemShadowImage = icodi.ShadowImage;
                            }
                        }
                        else
                        {
                            if (icodi.CheckedImage != null)
                            {
                                ItemDisabledImage = icodi.CheckedDisabledImage;
                            }
                            else
                            {
                                ItemDisabledImage = icodi.DisabledImage;
                            }
                        }
                    }
                    else
                    {
                        if (ItemEnabled)
                        {
                            ItemImage = icodi.Image;
                            ItemShadowImage = icodi.ShadowImage;
                        }
                        else
                            ItemDisabledImage = icodi.DisabledImage;
                    }
                }
                else
                {
                    if (ItemEnabled)
                    {
                        ItemImage = e.Item.Image;
                        ItemShadowImage = e.Item.ShadowImage;
                    }
                    else
                        ItemDisabledImage = e.Item.DisabledImage;
                }
                if (ItemSelected)
                {
                    e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.AssumeLinear;
                    e.Graphics.FillRectangle(SelectionAreaBrush, e.Bounds);
                    e.Graphics.DrawRectangle(SystemPens.Highlight, e.Bounds.X, e.Bounds.Y, e.Bounds.Width - 1, e.Bounds.Height - 1);
                    if (ItemEnabled && ItemImage != null && ItemShadowImage != null)
                    {
                        RectangleF ShadowArea = new RectangleF(new PointF(ImageArea.Left + 4, ImageArea.Top + (ImageArea.Height - 16) / 2), new SizeF(16, 16));
                        RectangleF FloatArea = new RectangleF(new PointF(ImageArea.Left + 2, ImageArea.Top + (ImageArea.Height - 16) / 2 - 2), new SizeF(16, 16));
                        if (ItemChecked)
                        {
                            System.Drawing.Brush CheckedBackBrush = new System.Drawing.SolidBrush(ImageAreaCheckColor);
                            Rectangle ImageBackArea = new Rectangle(new Point((int)(ImageArea.Left + 1), (int)(ImageArea.Top + (ImageArea.Height - 16) / 2 - 3)), new Size(20, 20));
                            e.Graphics.FillRectangle(CheckedBackBrush, ImageBackArea);
                            e.Graphics.DrawRectangle(SystemPens.Highlight, ImageBackArea);
                            e.Graphics.DrawImage(ItemImage, ShadowArea);
                            if (CheckedBackBrush != null)
                                CheckedBackBrush.Dispose();
                            CheckedBackBrush = null;
                        }
                        else
                        {
                            if ((!(ItemSeparator)) && e.Item is IOwnerDrawnCheckBoxItem<TDrawnItem>)
                            {
                                System.Drawing.Brush CheckedBackBrush;
                                if (((IOwnerDrawnCheckBoxItem<TDrawnItem>)e.Item).CheckAreaHovered)
                                    CheckedBackBrush = new System.Drawing.SolidBrush(SelAreaBackColor);
                                else
                                    CheckedBackBrush = new System.Drawing.SolidBrush(ImageAreaStartColor);
                                Rectangle ImageBackArea = new Rectangle(new Point((int)(ImageArea.Left + 1), (int)(ImageArea.Top + (ImageArea.Height - 16) / 2 - 3)), new Size(20, 20));
                                e.Graphics.FillRectangle(CheckedBackBrush, ImageBackArea);
                                e.Graphics.DrawRectangle(SystemPens.Highlight, ImageBackArea);
                            }
                            e.Graphics.DrawImage(ItemShadowImage, ShadowArea);
                            e.Graphics.DrawImage(ItemImage, FloatArea);
                        }
                    }
                    else if (ItemDisabledImage != null)
                    {
                        RectangleF FloatArea = new RectangleF(new PointF(4, ImageArea.Top + (ImageArea.Height - 16) / 2), new SizeF(16, 16));
                        e.Graphics.DrawImage(ItemDisabledImage, FloatArea);
                    }
                    else if ((!(ItemSeparator)) && e.Item is IOwnerDrawnCheckBoxItem<TDrawnItem>)
                    {
                        System.Drawing.Brush CheckedBackBrush;
                        if (((IOwnerDrawnCheckBoxItem<TDrawnItem>)e.Item).CheckAreaHovered)
                            CheckedBackBrush = new System.Drawing.SolidBrush(SelAreaBackColor);
                        else
                            CheckedBackBrush = new System.Drawing.SolidBrush(ImageAreaStartColor);
                        Rectangle ImageBackArea = new Rectangle(new Point((int)(ImageArea.Left + 1), (int)(ImageArea.Top + (ImageArea.Height - 16) / 2 - 3)), new Size(20, 20));
                        e.Graphics.FillRectangle(CheckedBackBrush, ImageBackArea);
                        e.Graphics.DrawRectangle(SystemPens.Highlight, ImageBackArea);
                    }
                }
                else
                {
                    e.Graphics.FillRectangle(ImageBackAreaBrush, ImageArea);
                    e.Graphics.FillRectangle(TextBackAreaBrush, TextArea);
                    if (ItemImage != null && ItemEnabled)
                    {
                        RectangleF FloatArea = new RectangleF(new PointF(ImageArea.Left + 4, ImageArea.Top + (ImageArea.Height - 16) / 2), new SizeF(16, 16));
                        if (ItemChecked)
                        {
                            System.Drawing.Brush CheckedBackBrush = new System.Drawing.SolidBrush(SelAreaBackColor);
                            Rectangle ImageBackArea = new Rectangle(new Point((int)(ImageArea.Left + 1), (int)(ImageArea.Top + (ImageArea.Height - 16) / 2 - 3)), new Size(20, 20));
                            e.Graphics.FillRectangle(CheckedBackBrush, ImageBackArea);
                            e.Graphics.DrawRectangle(SystemPens.Highlight, ImageBackArea);
                            if (CheckedBackBrush != null)
                                CheckedBackBrush.Dispose();
                            CheckedBackBrush = null;
                        }
                        else if ((!(ItemSeparator)) && e.Item is IOwnerDrawnCheckBoxItem<TDrawnItem>)
                        {
                            System.Drawing.Brush CheckedBackBrush;
                            if (((IOwnerDrawnCheckBoxItem<TDrawnItem>)e.Item).CheckAreaHovered)
                                CheckedBackBrush = new System.Drawing.SolidBrush(SelAreaBackColor);
                            else
                                CheckedBackBrush = new System.Drawing.SolidBrush(ImageAreaStartColor);
                            Rectangle ImageBackArea = new Rectangle(new Point((int)(ImageArea.Left + 1), (int)(ImageArea.Top + (ImageArea.Height - 16) / 2 - 3)), new Size(20, 20));
                            e.Graphics.FillRectangle(CheckedBackBrush, ImageBackArea);
                            e.Graphics.DrawRectangle(SystemPens.Highlight, ImageBackArea);
                        }
                        e.Graphics.DrawImage(ItemImage, FloatArea);
                    }
                    else if (ItemDisabledImage != null && (!ItemEnabled))
                    {
                        RectangleF FloatArea = new RectangleF(new PointF(4, ImageArea.Top + (ImageArea.Height - 16) / 2), new SizeF(16, 16));
                        e.Graphics.DrawImage(ItemDisabledImage, FloatArea);
                    }
                    else if ((!(ItemSeparator)) && e.Item is IOwnerDrawnCheckBoxItem<TDrawnItem>)
                    {
                        System.Drawing.Brush CheckedBackBrush;
                        if (((IOwnerDrawnCheckBoxItem<TDrawnItem>)e.Item).CheckAreaHovered)
                            CheckedBackBrush = new System.Drawing.SolidBrush(SelAreaBackColor);
                        else
                            CheckedBackBrush = new System.Drawing.SolidBrush(ImageAreaStartColor);
                        Rectangle ImageBackArea = new Rectangle(new Point((int)(ImageArea.Left + 1), (int)(ImageArea.Top + (ImageArea.Height - 16) / 2 - 3)), new Size(20, 20));
                        e.Graphics.FillRectangle(CheckedBackBrush, ImageBackArea);
                        e.Graphics.DrawRectangle(SystemPens.Highlight, ImageBackArea);
                    }
                }
                ItemShadowImage = null;
                ItemDisabledImage = null;
                ItemImage = null;
                TextArea.X += 6;
                if (!ItemSeparator)
                {
                    if (menuItem == null && ItemFocused && ItemSelected)
                    {
                        System.Drawing.SizeF textSize = e.Graphics.MeasureString(ItemText, e.Font);
                        System.Drawing.Rectangle textFocusRect = new Rectangle((int)TextArea.X, (int)TextArea.Y + (int)(TextArea.Height - textSize.Height) / 2, (int)textSize.Width, (int)textSize.Height);
                        System.Drawing.Pen textPen = new Pen(new HatchBrush(HatchStyle.Percent50, Color.Black, Color.Transparent), 1);
                        e.Graphics.DrawRectangle(textPen, textFocusRect);
                        textPen.Dispose();
                    }
                    e.Graphics.DrawString(ItemText, e.Font, StringBrush, TextArea, ItemStrFormat);
                    ItemStrFormat.Alignment = StringAlignment.Far;
                    if (ItemShortcut != null && ItemShortcut != "")
                    {
                        TextArea.Width -= 24;
                        e.Graphics.DrawString(ItemShortcut, e.Font, StringBrush, TextArea, ItemStrFormat);
                    }
                }
                else
                {
                    e.Graphics.DrawLine(SystemPens.ControlDark, new PointF(TextArea.X, TextArea.Y + (e.Bounds.Height / 2)), new PointF(e.Bounds.Right, TextArea.Y + (e.Bounds.Height / 2)));
                    if (e.Item.Text != "-")
                    {
                        String subString = e.Item.Text.Substring(1);
                        SizeF textSize = e.Graphics.MeasureString(subString, e.Font);
                        RectangleF SeparatorText = new RectangleF(TextArea.X + (TextArea.Width - textSize.Width) / 2, TextArea.Top + (TextArea.Height - textSize.Height) / 2, textSize.Width, textSize.Height);
                        e.Graphics.FillRectangle(TextBackAreaBrush, SeparatorText);
                        e.Graphics.DrawString(subString, e.Font, StringBrush, SeparatorText, ItemStrFormat);
                    }
                }
            }

            if (SelectionAreaBrush != null)
                SelectionAreaBrush.Dispose();
            SelectionAreaBrush = null;
            if (StringBrush != null)
                StringBrush.Dispose();
            StringBrush = null;
            if (TextBackAreaBrush != null)
                TextBackAreaBrush.Dispose();
            TextBackAreaBrush = null;
            if (ImageBackAreaBrush != null)
                ImageBackAreaBrush.Dispose();
            ImageBackAreaBrush = null;
            menuItem = null;
        }

        /// <summary>
        /// Informs the <see cref="OwnerDrawnSimpleStyle{TDrawnItem}"/> to draw
        /// a specific element as described within the arguments <paramref name="e"/>.
        /// </summary>
        /// <param name="e">The <see cref="IOwnerDrawnItemMeasureEventArgs{TDrawnItem}"/>
        /// which denote the specific item and other elements associated with the 
        /// metric calculation to be performed.</param>
        public override void OnMeasureItem(ref IOwnerDrawnItemMeasureEventArgs<TDrawnItem> e)
        {
            //Notes to self:
            //Each referencePoint is approximately 60 pixels + the width of the shortcut text and text.
            //There is a 24 pixel border on the right side
            //There is a 24 pixel rectangle for the image
            //There is an 8 pixel margin from the right of the image before the text starts.
            #region Setup
            Size Result = new Size();

            bool ItemSeparator = (e.Item.Text.Substring(0, 1) == "-");
            bool ItemTopmost = e.TopLevel;
            SizeF ItemTextSize;
            #endregion
            int tabChar = 0;
            String ItemText = "";
            String ItemShortcut = "";
            IOwnerDrawnMenuItem<TDrawnItem> menuItem = null;
            if (e.Item is IOwnerDrawnMenuItem<TDrawnItem>)
                menuItem = (IOwnerDrawnMenuItem<TDrawnItem>)e.Item;
            if (!ItemTopmost)
            {
                tabChar = e.Item.Text.IndexOf("\t", 0);
                if (tabChar == -1 && menuItem != null)
                {
                    ItemText = e.Item.Text;
                    ItemShortcut = GetShortcutText(menuItem.Shortcut);
                }
                else if (tabChar != -1)
                {
                    ItemText = e.Item.Text.Substring(0, tabChar);
                    ItemShortcut = e.Item.Text.Substring(tabChar + 1);
                }
                else
                {
                    ItemText = e.Item.Text;
                }
            }
            else
            {
                ItemText = e.Item.Text;
            }
            ItemTextSize = e.Graphics.MeasureString(ItemText, e.Font);
            if (ItemSeparator)
            {
                Result.Width = (int)ItemTextSize.Width;
                if (e.Item.Text != "-")
                {
                    Result.Height = (int)ItemTextSize.Height + 4;
                    if (menuItem != null && (menuItem.Enabled))
                        menuItem.Enabled = false;
                }
                else
                {
                    Result.Height = 4;
                }
            }
            else
            {
                Result.Width = (int)ItemTextSize.Width;
                if (ItemTopmost)
                    Result.Width -= 8;
                else
                {
                    Result.Width += 60;
                    if (ItemShortcut != null)
                    {
                        Result.Width += (int)e.Graphics.MeasureString(ItemShortcut, e.Font).Width;
                    }
                    else
                    {
                        Result.Width -= 15;
                    }
                }
                if (ItemTextSize.Height >= 20)
                    Result.Height = (int)ItemTextSize.Height + 4;
                else
                    Result.Height = 24;
            }
            e.Height = Result.Height;
            e.Width = Result.Width;
        }
    }
}

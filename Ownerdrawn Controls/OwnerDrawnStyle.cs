using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AllenCopeland.Abstraction.OwnerDrawnControls.Properties;
using StatesByIndex = System.Collections.Generic.Dictionary<int, AllenCopeland.Abstraction.OwnerDrawnControls.OwnerDrawnItemImageState>;
using StatesByString = System.Collections.Generic.Dictionary<string, AllenCopeland.Abstraction.OwnerDrawnControls.OwnerDrawnItemImageState>;
 /*----------------------------------------\
 | Copyright © 2011 Allen Copeland Jr.     |
 |-----------------------------------------|
 | The Abstraction Project's code is prov- |
 | -ided under a contract-release basis.   |
 | DO NOT DISTRIBUTE and do not use beyond |
 | the contract terms.                     |
 \--------------------------------------- */

namespace AllenCopeland.Abstraction.OwnerDrawnControls
{
    /// <summary>
    /// Provides a static class helper for owner drawn styles.
    /// </summary>
    public static class OwnerDrawnStyle
    {
        /// <summary>
        /// Custom DrawItemState used for hovered items.
        /// </summary>
        private static Bitmap defaultItemCheckedImage = null;
        private static Bitmap defaultItemCheckedImageShadow = null;
        private static Bitmap defaultItemCheckedImageDisabled = null;
        /// <summary>
        /// Dictionary that relates the imagelist to the list of cached images by their key.
        /// </summary>
        private static Dictionary<ImageList, StatesByString> keyImageStates = new Dictionary<ImageList, StatesByString>();
        /// <summary>
        /// Dictionary that relates the imagelist to the list of cached images by their index.
        /// </summary>
        private static Dictionary<ImageList, StatesByIndex> indexImageStates = new Dictionary<ImageList, StatesByIndex>();
        private static Dictionary<Bitmap, OwnerDrawnItemImageState> bitmapImageStates = new Dictionary<Bitmap, OwnerDrawnItemImageState>();
        private static List<ImageList> watchedImageLists = new List<ImageList>();
        /// <summary>
        /// Retrieves or caches and returns the image and sub-image states associated with an imagelist's
        /// specific image (via key).
        /// </summary>
        /// <param name="imagelist">The imagelist to source the images from..</param>
        /// <param name="image">The key to the image.</param>
        /// <returns>A <see cref="OwnerDrawnItemImageState"/> containing the Normal, Shadow and Disabled states of an image.</returns>
        [DebuggerHidden]
        public static OwnerDrawnItemImageState RetrieveOrCacheImage(ImageList imagelist, string image)
        {
            //Check to need to see if the imagelist has been encountered before...
            if (!keyImageStates.ContainsKey(imagelist))
            {
                if (!watchedImageLists.Contains(imagelist))
                {
                    imagelist.Disposed += new EventHandler(imagelist_Disposed);
                    watchedImageLists.Add(imagelist);
                }
                keyImageStates.Add(imagelist, new Dictionary<string, OwnerDrawnItemImageState>());
            }
            //Check if the image has been encountered before...
            if (!keyImageStates[imagelist].ContainsKey(image))
            {
                //When the key exists on the imagelist, obtain a copy of the image
                //in a System.Drawing.Bitmap object and process the disabled/shadow states.
                if (imagelist.Images.ContainsKey(image))
                {
                    Bitmap normalImage = new Bitmap(imagelist.Images[image]), disabledImage = null, shadowImage = null;
                    ProcessStyles(normalImage, ref disabledImage, ref shadowImage);
                    OwnerDrawnItemImageState imageState = new OwnerDrawnItemImageState(normalImage, disabledImage, shadowImage);
                    keyImageStates[imagelist].Add(image, imageState);
                    return imageState;
                }
                else //No matching key exists...
                    return new OwnerDrawnItemImageState();
            }
            else
            { //return the cached image.
                return keyImageStates[imagelist][image];
            }
        }

        static void imagelist_Disposed(object sender, EventArgs e)
        {
            ImageList ilSender = (sender as ImageList);
            if (ilSender == null)
                return;
            ilSender.Disposed -= new EventHandler(imagelist_Disposed);
            StatesByIndex indexedStates = null;
            if (indexImageStates.ContainsKey(ilSender))
                indexedStates = indexImageStates[ilSender];
            StatesByString keyedStates = null;
            if (keyImageStates.ContainsKey(ilSender))
                keyedStates = keyImageStates[ilSender];
            IEnumerable<OwnerDrawnItemImageState> states = null;
            if (indexedStates == null && keyedStates != null)
                states = keyedStates.Values;
            else if (keyedStates == null && indexedStates != null)
                states = indexedStates.Values;
            else if (indexedStates != null && keyedStates != null)
                states = indexedStates.Values.Concat(keyedStates.Values);
            else
                return;
            if (watchedImageLists.Contains(ilSender))
                watchedImageLists.Remove(ilSender);
            foreach (var k in states)
                k.Dispose();
            if (indexImageStates.ContainsKey(ilSender))
                indexImageStates.Remove(ilSender);
            if (keyImageStates.ContainsKey(ilSender))
                keyImageStates.Remove(ilSender);
        }

        /// <summary>
        /// Retrieves or caches and returns the image and sub-image states associated with an imagelist's
        /// specific image (via index).
        /// </summary>
        /// <param name="imagelist">The imagelist to source the images from..</param>
        /// <param name="image">The index of the image.</param>
        /// <returns>A <see cref="OwnerDrawnItemImageState"/> containing the Normal, Shadow and Disabled states of an image.</returns>
        [DebuggerHidden]
        public static OwnerDrawnItemImageState RetrieveOrCacheImage(ImageList imagelist, int image)
        {
            //Check to need to see if the imagelist has been encountered before...
            if (!indexImageStates.ContainsKey(imagelist))
            {
                if (!watchedImageLists.Contains(imagelist))
                {
                    imagelist.Disposed += new EventHandler(imagelist_Disposed);
                    watchedImageLists.Add(imagelist);
                }
                indexImageStates.Add(imagelist, new Dictionary<int, OwnerDrawnItemImageState>());
            }
            //Check if the image has been encountered before...
            if (!indexImageStates[imagelist].ContainsKey(image))
            {
                //When the index is in range, obtain a copy of the image
                //in a System.Drawing.Bitmap object and process the disabled/shadow states.
                if ((image < imagelist.Images.Count) && (image >= 0))
                {
                    Bitmap normalImage = new Bitmap(imagelist.Images[image]), disabledImage = null, shadowImage = null;
                    ProcessStyles(normalImage, ref disabledImage, ref shadowImage);
                    OwnerDrawnItemImageState imageState = new OwnerDrawnItemImageState(normalImage, disabledImage, shadowImage);
                    indexImageStates[imagelist].Add(image, imageState);
                    return imageState;
                }
                else //Out of range...
                    return new OwnerDrawnItemImageState();
            }
            else
            { //return the cached image.
                return indexImageStates[imagelist][image];
            }
        }
        /// <summary>
        /// Disposes of a specific triple image state associated to an image list by index.
        /// </summary>
        /// <param name="imageList">The <see cref="ImageList"/> which relates
        /// to the specific image list to remove the element of.</param>
        /// <param name="image">The <see cref="Int32"/> ordinal index relative to
        /// <paramref name="imageList"/> to dispose.</param>
        public static void DisposeOfCachedImage(ImageList imageList, int image)
        {
            //Check to need to see if the imagelist has been encountered before...
            if (!indexImageStates.ContainsKey(imageList))
                return;
            if (indexImageStates[imageList].ContainsKey(image))
            {
                var state = indexImageStates[imageList][image];
                indexImageStates[imageList].Remove(image);
                state.Dispose();
                state = null;
            }

        }

        /// <summary>
        /// Disposes the triple state of the <paramref name="image"/> provided.
        /// </summary>
        /// <param name="image">The <see cref="Bitmap"/> to dispose the triple state of.</param>
        public static void DisposeOfCachedImage(Bitmap image)
        {
            if (bitmapImageStates.ContainsKey(image))
                bitmapImageStates[image].Dispose();
        }

        /// <summary>
        /// Obtains the <see cref="OwnerDrawnItemImageState"/> instance which represents
        /// the triple state of the <paramref name="image"/> provided with the
        /// <paramref name="transparencyColor"/> applied to all states.
        /// </summary>
        /// <param name="image">The <see cref="Bitmap"/> image which seeds the triple state.</param>
        /// <param name="transparencyColor">The <see cref="Color"/> which designates what color
        /// to make transparent during processing.</param>
        /// <returns>A new <see cref="OwnerDrawnItemImageState"/> which represents the triple
        /// state of the <paramref name="image"/> provided.</returns>
        [DebuggerHidden]
        public static OwnerDrawnItemImageState RetrieveOrCacheImage(Bitmap image, Color transparencyColor)
        {
            if (!(bitmapImageStates.ContainsKey(image)))
            {
                //When the image Item isn't inside the state dictionary, copy the base,
                //and process sub-styles, then insert it into the cache.
                Bitmap normalImage = new Bitmap((Image)image), disabledImage = null, shadowImage = null;
                normalImage.MakeTransparent(transparencyColor);
                //Used to process shadow/disabled states.
                ProcessStyles(normalImage, ref disabledImage, ref shadowImage);
                OwnerDrawnItemImageState imageState = new OwnerDrawnItemImageState(normalImage, disabledImage, shadowImage);
                bitmapImageStates.Add(image, imageState);
                return imageState;
            }
            else
            {//Return the cached image.
                return bitmapImageStates[image];
            }
        }
        [DebuggerHidden]
        private static void ProcessDefaultCheckedImage()
        {
            if (defaultItemCheckedImage == null)
            {
                defaultItemCheckedImage = (System.Drawing.Bitmap)Resources.CheckDefault.Clone();
                defaultItemCheckedImage.MakeTransparent(Color.Aqua);
                ProcessStyles(defaultItemCheckedImage, ref defaultItemCheckedImageDisabled, ref defaultItemCheckedImageShadow);
            }
        }

        /// <summary>
        /// Returns the default checked image of the any item which is owner drawn.
        /// </summary>
        public static System.Drawing.Bitmap DefaultItemCheckedImage
        {
            [DebuggerHidden]
            get
            {
                ProcessDefaultCheckedImage();
                return defaultItemCheckedImage;
            }
        }

        /// <summary>
        /// Returns the default shadow image of any item which is owner drawn.
        /// </summary>
        public static System.Drawing.Bitmap DefaultItemCheckedShadowImage
        {
            [DebuggerHidden]
            get
            {
                ProcessDefaultCheckedImage();
                return defaultItemCheckedImageShadow;
            }
        }
        /// <summary>
        /// Returns the default disabled image of any item which is owner drawn.
        /// </summary>
        public static System.Drawing.Bitmap DefaultItemCheckedDisabledImage
        {
            [DebuggerHidden]
            get
            {
                ProcessDefaultCheckedImage();
                return defaultItemCheckedImageDisabled;
            }
        }
        /// <summary>
        /// Processes the Disabled and Shadow states of a given image.
        /// </summary>
        /// <param name="source">The source image</param>
        /// <param name="Disabled">The destination Disabled image.</param>
        /// <param name="Shadowed">The destination Shadowed image.</param>
        [DebuggerHidden]
        public static void ProcessStyles(Bitmap source, ref Bitmap Disabled, ref Bitmap Shadowed)
        {
            Color shadowColor = SystemColors.ControlDarkDark;
            //First we need to ensure the image is valid...
            if (source != null)
            {
                //Prepare two copies of the image, mostly to ensure equal size.
                System.Drawing.Bitmap disabled = (Bitmap)source.Clone();
                System.Drawing.Bitmap shadow = (System.Drawing.Bitmap)source.Clone();
                try
                {
                    //We start the loop vertically first so it goes from top to 
                    //bottom, the internal loop is horizontal to ensure 
                    //an order of right->left and top->bottom respectively.
                    //Loop through it vertically
                    for (int y = 0; y < shadow.Height; y++)
                        //Loop through it horizontally
                        for (int x = 0; x < shadow.Width; x++)
                        {
                            //There's really no special logic to processing these. 
                            //I just messed around with this until I liked it.
                            System.Drawing.Color mC = source.GetPixel(x, y);
                            int avg = 255 - (int)((float)Math.Min(Math.Min(mC.R, mC.B), mC.G) + (float)Math.Max(Math.Max(mC.R, mC.B), mC.G)) / 2;
                            int alpha = (int)(((float)mC.A * ((float)avg / 255)) / 255 * 128) + ((mC.A == 0) ? 0 : 64);
                            shadow.SetPixel(x, y, System.Drawing.Color.FromArgb(alpha, (int)((float)mC.R / 255F * (float)shadowColor.R), (int)((float)mC.G / 255F * (float)shadowColor.G), (int)((float)mC.B / 255F * (float)shadowColor.B)));
                            disabled.SetPixel(x, y, System.Drawing.Color.FromArgb((int)((mC.A == 0) ? 0 : (128 - (mC.GetBrightness() * 128))), mC.R, mC.G, mC.B));
                        }
                }
                catch (System.Exception e) { System.Console.WriteLine(e.ToString()); }
                Shadowed = shadow;
                Disabled = disabled;
            }
        }
        [DebuggerHidden]
        internal static Color Adjust(this Color original, int r, int g, int b)
        {
            return Color.FromArgb(original.A, Math.Max(original.R + r, 0), Math.Max(original.G + g, 0), Math.Max(original.B + b, 0));
        }

    }
    /// <summary>
    /// Provides a base partial implementation of an <see cref="IOwnerDrawnStyle{TDrawnItem}"/>.
    /// </summary>
    /// <typeparam name="TDrawnItem">The kind of <see cref="IOwnerDrawnItem"/>
    /// that is measured/rendered by the <see cref="OwnerDrawnStyle{TDrawnStyle}"/>.</typeparam>
    public abstract class OwnerDrawnStyle<TDrawnItem> : IOwnerDrawnStyle<TDrawnItem>
        where TDrawnItem :
            IOwnerDrawnItem<TDrawnItem>
    {
        #region IOwnerDrawnStyle<TItem,TDrawnItem> Members

        /// <summary>
        /// Informs the <see cref="OwnerDrawnStyle{TDrawnItem}"/> to draw a
        /// specific element as defined within the arguments <paramref name="e"/>.
        /// </summary>
        /// <param name="e">The <see cref="IOwnerDrawnItemDrawEventArgs{TDrawnItem}"/>
        /// which denote the specific item and other elements
        /// associated with the drawing operation to be performed.</param>
        public abstract void OnDrawItem(IOwnerDrawnItemDrawEventArgs<TDrawnItem> e);

        /// <summary>
        /// Informs the <see cref="OwnerDrawnStyle{TDrawnItem}"/> to draw
        /// a specific element as described within the arguments <paramref name="e"/>.
        /// </summary>
        /// <param name="e">The <see cref="IOwnerDrawnItemMeasureEventArgs{TDrawnItem}"/>
        /// which denote the specific item and other elements associated with the 
        /// metric calculation to be performed.</param>
        public abstract void OnMeasureItem(ref IOwnerDrawnItemMeasureEventArgs<TDrawnItem> e);

        #endregion
        [DebuggerHidden]
        internal void OnDrawItemEvent(TDrawnItem item, DrawItemEventArgs args, Font font)
        {
            IOwnerDrawnItemDrawEventArgs<TDrawnItem> arguments;
            if (item is IOwnerDrawnMenuItem<TDrawnItem>)
                arguments = new OwnerDrawnItemDrawEventArgs<TDrawnItem>(args, args.Bounds, args.Graphics, item, font, ((IOwnerDrawnMenuItem<TDrawnItem>)item).TopMost);
            else
                arguments =  new OwnerDrawnItemDrawEventArgs<TDrawnItem>(args, args.Bounds, args.Graphics, item, font);
            this.OnDrawItem(arguments);
        }
        [DebuggerHidden]
        internal void OnMeasureItemEvent(TDrawnItem item, ref MeasureItemEventArgs args, Font font)
        {
            IOwnerDrawnItemMeasureEventArgs<TDrawnItem> arguments;
            if (item is IOwnerDrawnMenuItem<TDrawnItem>)
                arguments = new OwnerDrawnItemMeasureEventArgs<TDrawnItem>(args.ItemHeight, args.Graphics, item, font, ((IOwnerDrawnMenuItem<TDrawnItem>)item).TopMost);
            else
                arguments = new OwnerDrawnItemMeasureEventArgs<TDrawnItem>(args.ItemHeight, args.Graphics, item, font);
            this.OnMeasureItem(ref arguments);
            args.ItemWidth = arguments.Width;
            args.ItemHeight = arguments.Height;
        }

        /// <summary>
        /// Obtains the <see cref="String"/> value of the <paramref name="shortcut"/>
        /// provided.
        /// </summary>
        /// <param name="shortcut">The <see cref="Shortcut"/>
        /// which denotes a key combination to obtain the string for.</param>
        /// <returns>A <see cref="String"/> which relates to the accelerator keys needed
        /// to activate a given <paramref name="shortcut"/>.</returns>
        [DebuggerHidden]
        public static string GetShortcutText(Shortcut shortcut)
        {
            string str = "";
            switch (shortcut)
            {
                case Shortcut.F1: str = "F1"; break;
                case Shortcut.F2: str = "F2"; break;
                case Shortcut.F3: str = "F3"; break;
                case Shortcut.F4: str = "F4"; break;
                case Shortcut.F5: str = "F5"; break;
                case Shortcut.F6: str = "F6"; break;
                case Shortcut.F7: str = "F7"; break;
                case Shortcut.F8: str = "F8"; break;
                case Shortcut.F9: str = "F9"; break;
                case Shortcut.F10: str = "F10"; break;
                case Shortcut.F11: str = "F11"; break;
                case Shortcut.F12: str = "F12"; break;
                case Shortcut.Alt0: str = "Alt+0"; break;
                case Shortcut.Alt1: str = "Alt+1"; break;
                case Shortcut.Alt2: str = "Alt+2"; break;
                case Shortcut.Alt3: str = "Alt+3"; break;
                case Shortcut.Alt4: str = "Alt+4"; break;
                case Shortcut.Alt5: str = "Alt+5"; break;
                case Shortcut.Alt6: str = "Alt+6"; break;
                case Shortcut.Alt7: str = "Alt+7"; break;
                case Shortcut.Alt8: str = "Alt+8"; break;
                case Shortcut.Alt9: str = "Alt+9"; break;
                case Shortcut.Ctrl0: str = "Ctrl+0"; break;
                case Shortcut.Ctrl1: str = "Ctrl+1"; break;
                case Shortcut.Ctrl2: str = "Ctrl+2"; break;
                case Shortcut.Ctrl3: str = "Ctrl+3"; break;
                case Shortcut.Ctrl4: str = "Ctrl+4"; break;
                case Shortcut.Ctrl5: str = "Ctrl+5"; break;
                case Shortcut.Ctrl6: str = "Ctrl+6"; break;
                case Shortcut.Ctrl7: str = "Ctrl+7"; break;
                case Shortcut.Ctrl8: str = "Ctrl+8"; break;
                case Shortcut.Ctrl9: str = "Ctrl+9"; break;
                case Shortcut.CtrlShift0: str = "Ctrl+Shift+0"; break;
                case Shortcut.CtrlShift1: str = "Ctrl+Shift+1"; break;
                case Shortcut.CtrlShift2: str = "Ctrl+Shift+2"; break;
                case Shortcut.CtrlShift3: str = "Ctrl+Shift+3"; break;
                case Shortcut.CtrlShift4: str = "Ctrl+Shift+4"; break;
                case Shortcut.CtrlShift5: str = "Ctrl+Shift+5"; break;
                case Shortcut.CtrlShift6: str = "Ctrl+Shift+6"; break;
                case Shortcut.CtrlShift7: str = "Ctrl+Shift+7"; break;
                case Shortcut.CtrlShift8: str = "Ctrl+Shift+8"; break;
                case Shortcut.CtrlShift9: str = "Ctrl+Shift+9"; break;
                case Shortcut.CtrlA: str = "Ctrl+A"; break;
                case Shortcut.CtrlB: str = "Ctrl+B"; break;
                case Shortcut.CtrlC: str = "Ctrl+C"; break;
                case Shortcut.CtrlD: str = "Ctrl+D"; break;
                case Shortcut.CtrlE: str = "Ctrl+E"; break;
                case Shortcut.CtrlF: str = "Ctrl+F"; break;
                case Shortcut.CtrlG: str = "Ctrl+G"; break;
                case Shortcut.CtrlH: str = "Ctrl+H"; break;
                case Shortcut.CtrlI: str = "Ctrl+I"; break;
                case Shortcut.CtrlJ: str = "Ctrl+J"; break;
                case Shortcut.CtrlK: str = "Ctrl+K"; break;
                case Shortcut.CtrlL: str = "Ctrl+L"; break;
                case Shortcut.CtrlM: str = "Ctrl+M"; break;
                case Shortcut.CtrlN: str = "Ctrl+N"; break;
                case Shortcut.CtrlO: str = "Ctrl+O"; break;
                case Shortcut.CtrlP: str = "Ctrl+P"; break;
                case Shortcut.CtrlQ: str = "Ctrl+Q"; break;
                case Shortcut.CtrlR: str = "Ctrl+R"; break;
                case Shortcut.CtrlS: str = "Ctrl+S"; break;
                case Shortcut.CtrlT: str = "Ctrl+T"; break;
                case Shortcut.CtrlU: str = "Ctrl+U"; break;
                case Shortcut.CtrlV: str = "Ctrl+V"; break;
                case Shortcut.CtrlW: str = "Ctrl+W"; break;
                case Shortcut.CtrlX: str = "Ctrl+X"; break;
                case Shortcut.CtrlY: str = "Ctrl+Y"; break;
                case Shortcut.CtrlZ: str = "Ctrl+Z"; break;
                case Shortcut.CtrlShiftA: str = "Ctrl+Shift+A"; break;
                case Shortcut.CtrlShiftB: str = "Ctrl+Shift+B"; break;
                case Shortcut.CtrlShiftC: str = "Ctrl+Shift+C"; break;
                case Shortcut.CtrlShiftD: str = "Ctrl+Shift+D"; break;
                case Shortcut.CtrlShiftE: str = "Ctrl+Shift+E"; break;
                case Shortcut.CtrlShiftF: str = "Ctrl+Shift+F"; break;
                case Shortcut.CtrlShiftG: str = "Ctrl+Shift+G"; break;
                case Shortcut.CtrlShiftH: str = "Ctrl+Shift+H"; break;
                case Shortcut.CtrlShiftI: str = "Ctrl+Shift+I"; break;
                case Shortcut.CtrlShiftJ: str = "Ctrl+Shift+J"; break;
                case Shortcut.CtrlShiftK: str = "Ctrl+Shift+K"; break;
                case Shortcut.CtrlShiftL: str = "Ctrl+Shift+L"; break;
                case Shortcut.CtrlShiftM: str = "Ctrl+Shift+M"; break;
                case Shortcut.CtrlShiftN: str = "Ctrl+Shift+N"; break;
                case Shortcut.CtrlShiftO: str = "Ctrl+Shift+O"; break;
                case Shortcut.CtrlShiftP: str = "Ctrl+Shift+P"; break;
                case Shortcut.CtrlShiftQ: str = "Ctrl+Shift+Q"; break;
                case Shortcut.CtrlShiftR: str = "Ctrl+Shift+R"; break;
                case Shortcut.CtrlShiftS: str = "Ctrl+Shift+S"; break;
                case Shortcut.CtrlShiftT: str = "Ctrl+Shift+T"; break;
                case Shortcut.CtrlShiftU: str = "Ctrl+Shift+U"; break;
                case Shortcut.CtrlShiftV: str = "Ctrl+Shift+V"; break;
                case Shortcut.CtrlShiftW: str = "Ctrl+Shift+W"; break;
                case Shortcut.CtrlShiftX: str = "Ctrl+Shift+X"; break;
                case Shortcut.CtrlShiftY: str = "Ctrl+Shift+Y"; break;
                case Shortcut.CtrlShiftZ: str = "Ctrl+Shift+Z"; break;
                case Shortcut.CtrlF1: str = "Ctrl+F1"; break;
                case Shortcut.CtrlF2: str = "Ctrl+F2"; break;
                case Shortcut.CtrlF3: str = "Ctrl+F3"; break;
                case Shortcut.CtrlF4: str = "Ctrl+F4"; break;
                case Shortcut.CtrlF5: str = "Ctrl+F5"; break;
                case Shortcut.CtrlF6: str = "Ctrl+F6"; break;
                case Shortcut.CtrlF7: str = "Ctrl+F7"; break;
                case Shortcut.CtrlF8: str = "Ctrl+F8"; break;
                case Shortcut.CtrlF9: str = "Ctrl+F9"; break;
                case Shortcut.CtrlF10: str = "Ctrl+F10"; break;
                case Shortcut.CtrlF11: str = "Ctrl+F11"; break;
                case Shortcut.CtrlF12: str = "Ctrl+F12"; break;
                case Shortcut.AltF1: str = "Alt+F1"; break;
                case Shortcut.AltF2: str = "Alt+F2"; break;
                case Shortcut.AltF3: str = "Alt+F3"; break;
                case Shortcut.AltF4: str = "Alt+F4"; break;
                case Shortcut.AltF5: str = "Alt+F5"; break;
                case Shortcut.AltF6: str = "Alt+F6"; break;
                case Shortcut.AltF7: str = "Alt+F7"; break;
                case Shortcut.AltF8: str = "Alt+F8"; break;
                case Shortcut.AltF9: str = "Alt+F9"; break;
                case Shortcut.AltF10: str = "Alt+F10"; break;
                case Shortcut.AltF11: str = "Alt+F11"; break;
                case Shortcut.AltF12: str = "Alt+F12"; break;
                case Shortcut.AltBksp: str = "Alt+Backspace"; break;
                case Shortcut.CtrlDel: str = "Ctrl+Delete"; break;
                case Shortcut.CtrlIns: str = "Ctrl+Insert"; break;
                case Shortcut.Del: str = "Delete"; break;
                case Shortcut.Ins: str = "Insert"; break;
                case Shortcut.ShiftDel: str = "Shift+Delete"; break;
                case Shortcut.ShiftF1: str = "Shift+F1"; break;
                case Shortcut.ShiftF2: str = "Shift+F2"; break;
                case Shortcut.ShiftF3: str = "Shift+F3"; break;
                case Shortcut.ShiftF4: str = "Shift+F4"; break;
                case Shortcut.ShiftF5: str = "Shift+F5"; break;
                case Shortcut.ShiftF6: str = "Shift+F6"; break;
                case Shortcut.ShiftF7: str = "Shift+F7"; break;
                case Shortcut.ShiftF8: str = "Shift+F8"; break;
                case Shortcut.ShiftF9: str = "Shift+F9"; break;
                case Shortcut.ShiftF10: str = "Shift+F10"; break;
                case Shortcut.ShiftF11: str = "Shift+F11"; break;
                case Shortcut.ShiftF12: str = "Shift+F12"; break;
                case Shortcut.CtrlShiftF1: str = "Ctrl+Shift+F1"; break;
                case Shortcut.CtrlShiftF2: str = "Ctrl+Shift+F2"; break;
                case Shortcut.CtrlShiftF3: str = "Ctrl+Shift+F3"; break;
                case Shortcut.CtrlShiftF4: str = "Ctrl+Shift+F4"; break;
                case Shortcut.CtrlShiftF5: str = "Ctrl+Shift+F5"; break;
                case Shortcut.CtrlShiftF6: str = "Ctrl+Shift+F6"; break;
                case Shortcut.CtrlShiftF7: str = "Ctrl+Shift+F7"; break;
                case Shortcut.CtrlShiftF8: str = "Ctrl+Shift+F8"; break;
                case Shortcut.CtrlShiftF9: str = "Ctrl+Shift+F9"; break;
                case Shortcut.CtrlShiftF10: str = "Ctrl+Shift+F10"; break;
                case Shortcut.CtrlShiftF11: str = "Ctrl+Shift+F11"; break;
                case Shortcut.CtrlShiftF12: str = "Ctrl+Shift+F12"; break;
                default:
                    str = "";
                    break;
            }
            return str;
        }

    }
}

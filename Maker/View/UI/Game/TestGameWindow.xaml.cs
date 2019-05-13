using Maker.Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

using System.IO;
using System.Windows.Media.Media3D;
using System.ComponentModel;
using System.Threading;
using Maker.Business;

namespace Maker.View.UI.Game
{
    /// <summary>
    /// PavedLaunchpadWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TestGameWindow : Window
    {
     
        private List<Model.Light> mLightList;
        public TestGameWindow()
        {
            InitializeComponent();

            //FileBusiness.CreateInstance().ReplaceControl(this.mLightList, FileBusiness.CreateInstance().midiArr);
            //for (int i = mLightList.Count - 1; i >= 0; i--)
            //{
            //    if (mLightList[i].Position < 36 || mLightList[i].Position > 99)
            //    {
            //        mLightList.Remove(mLightList[i]);
            //    }
            //}

            //worker.WorkerReportsProgress = true;
            //worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            //worker.ProgressChanged += worker_ProgressChanged;
            //worker.WorkerSupportsCancellation = true;
            //SetData(mLightList);
        }

        private void wMain_Loaded(object sender, RoutedEventArgs e)
        {
          
            InitData();

            //Bottom
            {
                GeometryModel3D geometryModel3D = new GeometryModel3D();
                MeshGeometry3D meshGeometry3D = new MeshGeometry3D();
                geometryModel3D.Geometry = meshGeometry3D;

                Point3DCollection point3DCollection = new Point3DCollection();
                point3DCollection.Add(new Point3D(-8.5, -1, -8.5));
                point3DCollection.Add(new Point3D(9, -1, -8.5));
                point3DCollection.Add(new Point3D(9, 1, -8.5));
                point3DCollection.Add(new Point3D(-8.5, 1, -8.5));
                point3DCollection.Add(new Point3D(-8.5, 1, 9));
                point3DCollection.Add(new Point3D(-8.5, -1, 9));
                point3DCollection.Add(new Point3D(9, -1, 9));
                point3DCollection.Add(new Point3D(9, 1, 9));
                meshGeometry3D.Positions = point3DCollection;
                geometryModel3D.Material = new DiffuseMaterial(new SolidColorBrush(Colors.Black));
                meshGeometry3D.TriangleIndices = int32Collection;
                model3DGroup.Children.Add(geometryModel3D);
            }


            for (int j = 7; j >= 0; j--)
            {
                for (int i = 0; i < 4; i++)
                {
                    GeometryModel3D geometryModel3D = new GeometryModel3D();
                    geometryModel3D.Material = new DiffuseMaterial()
                    {
                        Brush = StaticConstant.closeBrush
                    };
                    MeshGeometry3D meshGeometry3D = new MeshGeometry3D();
                    geometryModel3D.Geometry = meshGeometry3D;

                    Point3DCollection point3DCollection = new Point3DCollection();
                    point3DCollection.Add(new Point3D(-7.5 + 2 * (i), 1, -7.5 + 2 * (j)));
                    point3DCollection.Add(new Point3D(-6 + 2 * (i), 1, -7.5 + 2 * (j)));
                    point3DCollection.Add(new Point3D(-6 + 2 * (i), 1.5, -7.5 + 2 * (j)));
                    point3DCollection.Add(new Point3D(-7.5 + 2 * (i), 1.5, -7.5 + 2 * (j)));
                    point3DCollection.Add(new Point3D(-7.5 + 2 * (i), 1.5, -6 + 2 * (j)));
                    point3DCollection.Add(new Point3D(-7.5 + 2 * (i), 1, -6 + 2 * (j)));
                    point3DCollection.Add(new Point3D(-6 + 2 * (i), 1, -6 + 2 * (j)));
                    point3DCollection.Add(new Point3D(-6 + 2 * (i), 1.5, -6 + 2 * (j)));
                    meshGeometry3D.Positions = point3DCollection;

                    meshGeometry3D.TriangleIndices = int32Collection;
                    model3DGroup.Children.Add(geometryModel3D);
                }
            }

            for (int j = 7; j >= 0; j--)
            {
                for (int i = 4; i < 8; i++)
                {
                    GeometryModel3D geometryModel3D = new GeometryModel3D();
                    geometryModel3D.Material = new DiffuseMaterial()
                    {
                        Brush = StaticConstant.closeBrush
                    };
                    MeshGeometry3D meshGeometry3D = new MeshGeometry3D();
                    geometryModel3D.Geometry = meshGeometry3D;

                    Point3DCollection point3DCollection = new Point3DCollection();
                    point3DCollection.Add(new Point3D(-7.5 + 2 * (i), 1, -7.5 + 2 * (j)));
                    point3DCollection.Add(new Point3D(-6 + 2 * (i), 1, -7.5 + 2 * (j)));
                    point3DCollection.Add(new Point3D(-6 + 2 * (i), 1.5, -7.5 + 2 * (j)));
                    point3DCollection.Add(new Point3D(-7.5 + 2 * (i), 1.5, -7.5 + 2 * (j)));
                    point3DCollection.Add(new Point3D(-7.5 + 2 * (i), 1.5, -6 + 2 * (j)));
                    point3DCollection.Add(new Point3D(-7.5 + 2 * (i), 1, -6 + 2 * (j)));
                    point3DCollection.Add(new Point3D(-6 + 2 * (i), 1, -6 + 2 * (j)));
                    point3DCollection.Add(new Point3D(-6 + 2 * (i), 1.5, -6 + 2 * (j)));
                    meshGeometry3D.Positions = point3DCollection;

                    meshGeometry3D.TriangleIndices = int32Collection;
                    model3DGroup.Children.Add(geometryModel3D);
                }
            }

            {
                GeometryModel3D geometryModel3D = new GeometryModel3D();
                MeshGeometry3D meshGeometry3D = new MeshGeometry3D();
                geometryModel3D.Geometry = meshGeometry3D;

                Point3DCollection point3DCollection = new Point3DCollection();
                point3DCollection.Add(new Point3D(-8.5, -16.5,11));
                point3DCollection.Add(new Point3D(9, -16.5, 11));
                point3DCollection.Add(new Point3D(9, 1, 11));
                point3DCollection.Add(new Point3D(-8.5,1, 11));
                point3DCollection.Add(new Point3D(-8.5, 1, 9));
                point3DCollection.Add(new Point3D(-8.5, -16.5, 9));
                point3DCollection.Add(new Point3D(9, -16.5, 9));
                point3DCollection.Add(new Point3D(9, 1, 9));
                meshGeometry3D.Positions = point3DCollection;
                geometryModel3D.Material = new DiffuseMaterial(new SolidColorBrush(Colors.Black));
                meshGeometry3D.TriangleIndices = int32Collection;
                model3DGroup.Children.Add(geometryModel3D);
            }


            for (int j = 0; j < 8; j++)
            {
                for (int i = 0; i < 4; i++)
                {
                    GeometryModel3D geometryModel3D = new GeometryModel3D();

                    geometryModel3D.Material = new DiffuseMaterial()
                    {
                        Brush = StaticConstant.closeBrush
                    };
                    MeshGeometry3D meshGeometry3D = new MeshGeometry3D();
                    geometryModel3D.Geometry = meshGeometry3D;

                    Point3DCollection point3DCollection = new Point3DCollection();
                    point3DCollection.Add(new Point3D(-6 + 2 * i, -15.5 + j * 2, 11));
                    point3DCollection.Add(new Point3D(-7.5 + 2 * i, -15.5 + j * 2, 11));
                    point3DCollection.Add(new Point3D(-7.5 + 2 * i, -14 + j * 2, 11));
                    point3DCollection.Add(new Point3D(-6 + 2 * i, -14 + j * 2, 11));
                    point3DCollection.Add(new Point3D(-6 + 2 * i, -14 + j * 2, 11.5));
                    point3DCollection.Add(new Point3D(-6 + 2 * i, -15.5 + j * 2, 11.5));
                    point3DCollection.Add(new Point3D(-7.5 + 2 * i, -15.5 + j * 2, 11.5));
                    point3DCollection.Add(new Point3D(-7.5 + 2*i, -14+ j*2, 11.5));
                    meshGeometry3D.Positions = point3DCollection;

                    meshGeometry3D.TriangleIndices = int32Collection;
                    model3DGroup.Children.Add(geometryModel3D);
                }
            }

            for (int j = 0; j < 8; j++)
            {
                for (int i = 4; i < 8; i++)
                {
                    GeometryModel3D geometryModel3D = new GeometryModel3D();

                    geometryModel3D.Material = new DiffuseMaterial()
                    {
                        Brush = StaticConstant.closeBrush
                    };
                    MeshGeometry3D meshGeometry3D = new MeshGeometry3D();
                    geometryModel3D.Geometry = meshGeometry3D;

                    Point3DCollection point3DCollection = new Point3DCollection();
                    point3DCollection.Add(new Point3D(-6 + 2 * i, -15.5 + j * 2, 11));
                    point3DCollection.Add(new Point3D(-7.5 + 2 * i, -15.5 + j * 2, 11));
                    point3DCollection.Add(new Point3D(-7.5 + 2 * i, -14 + j * 2, 11));
                    point3DCollection.Add(new Point3D(-6 + 2 * i, -14 + j * 2, 11));
                    point3DCollection.Add(new Point3D(-6 + 2 * i, -14 + j * 2, 11.5));
                    point3DCollection.Add(new Point3D(-6 + 2 * i, -15.5 + j * 2, 11.5));
                    point3DCollection.Add(new Point3D(-7.5 + 2 * i, -15.5 + j * 2, 11.5));
                    point3DCollection.Add(new Point3D(-7.5 + 2 * i, - 14 + j * 2, 11.5));
                    meshGeometry3D.Positions = point3DCollection;

                    meshGeometry3D.TriangleIndices = int32Collection;
                    model3DGroup.Children.Add(geometryModel3D);
                }
            }
            {
                GeometryModel3D geometryModel3D = new GeometryModel3D();
                MeshGeometry3D meshGeometry3D = new MeshGeometry3D();
                geometryModel3D.Geometry = meshGeometry3D;

                Point3DCollection point3DCollection = new Point3DCollection();
                point3DCollection.Add(new Point3D(9, -16.5, -8.5));
                point3DCollection.Add(new Point3D(9, 1, -8.5));
                point3DCollection.Add(new Point3D(11,1, -8.5));
                point3DCollection.Add(new Point3D(11, -16.5, -8.5));
                point3DCollection.Add(new Point3D(11 , -16.5, 9));
                point3DCollection.Add(new Point3D(9, -16.5, 9));
                point3DCollection.Add(new Point3D(9, 1, 9));
                point3DCollection.Add(new Point3D(11, 1, 9));
                meshGeometry3D.Positions = point3DCollection;
                geometryModel3D.Material = new DiffuseMaterial(new SolidColorBrush(Colors.Black));
                meshGeometry3D.TriangleIndices = int32Collection;
                model3DGroup.Children.Add(geometryModel3D);
            }
            for (int j = 0; j < 8; j++)
            {
                for (int i = 0; i < 4; i++)
                {
                    GeometryModel3D geometryModel3D = new GeometryModel3D();

                    geometryModel3D.Material = new DiffuseMaterial()
                    {
                        Brush = StaticConstant.closeBrush
                    };
                    MeshGeometry3D meshGeometry3D = new MeshGeometry3D();
                    geometryModel3D.Geometry = meshGeometry3D;

                    Point3DCollection point3DCollection = new Point3DCollection();
                    point3DCollection.Add(new Point3D(11.001, -15.5 + 2*j, 8 - 2 * i));
                    point3DCollection.Add(new Point3D(11.001, -14 + 2 * j, 8 - 2 * i));
                    point3DCollection.Add(new Point3D(11.5, -14 + 2 * j, 8 - 2 * i));
                    point3DCollection.Add(new Point3D(11.5, -15.5 + 2 * j, 8 - 2 * i));
                    point3DCollection.Add(new Point3D(11.5, -15.5 + 2 * j, 6.5 - 2 * i));
                    point3DCollection.Add(new Point3D(11.001, -15.5 + 2 * j, 6.5 - 2 * i));
                    point3DCollection.Add(new Point3D(11.001, -14 + 2 * j, 6.5 - 2 * i));
                    point3DCollection.Add(new Point3D(11.5, -14 + 2 * j, 6.5 - 2 * i));
                    meshGeometry3D.Positions = point3DCollection;

                    meshGeometry3D.TriangleIndices = int32Collection;
                    model3DGroup.Children.Add(geometryModel3D);
                }
            }

            for (int j = 0; j < 8; j++)
            {
                for (int i = 4; i < 8; i++)
                {
                    GeometryModel3D geometryModel3D = new GeometryModel3D();

                    geometryModel3D.Material = new DiffuseMaterial()
                    {
                        Brush = StaticConstant.closeBrush
                    };
                    MeshGeometry3D meshGeometry3D = new MeshGeometry3D();
                    geometryModel3D.Geometry = meshGeometry3D;

                    Point3DCollection point3DCollection = new Point3DCollection();
                    point3DCollection.Add(new Point3D(11.001, -15.5 + 2 * j, 8 - 2 * i));
                    point3DCollection.Add(new Point3D(11.001, -14 + 2 * j, 8 - 2 * i));
                    point3DCollection.Add(new Point3D(11.5, -14 + 2 * j, 8 - 2 * i));
                    point3DCollection.Add(new Point3D(11.5, -15.5 + 2 * j, 8 - 2 * i));
                    point3DCollection.Add(new Point3D(11.5, -15.5 + 2 * j, 6.5 - 2 * i));
                    point3DCollection.Add(new Point3D(11.001, -15.5 + 2 * j, 6.5 - 2 * i));
                    point3DCollection.Add(new Point3D(11.001, -14 + 2 * j, 6.5 - 2 * i));
                    point3DCollection.Add(new Point3D(11.5, -14 + 2 * j, 6.5 - 2 * i));
                    meshGeometry3D.Positions = point3DCollection;

                    meshGeometry3D.TriangleIndices = int32Collection;
                    model3DGroup.Children.Add(geometryModel3D);
                }
            }

            {
                GeometryModel3D geometryModel3D = new GeometryModel3D();
                MeshGeometry3D meshGeometry3D = new MeshGeometry3D();
                geometryModel3D.Geometry = meshGeometry3D;

                Point3DCollection point3DCollection = new Point3DCollection();
                point3DCollection.Add(new Point3D(11, -18.5, -8.5));
                point3DCollection.Add(new Point3D(28.5, -18.5, -8.5));
                point3DCollection.Add(new Point3D(28.5, -16.5, -8.5));
                point3DCollection.Add(new Point3D(11, -16.5, -8.5));
                point3DCollection.Add(new Point3D(11, -16.5, 9));
                point3DCollection.Add(new Point3D(11, -18.5, 9));
                point3DCollection.Add(new Point3D(28.5, -18.5, 9));
                point3DCollection.Add(new Point3D(28.5, -16.5, 9));
                meshGeometry3D.Positions = point3DCollection;
                geometryModel3D.Material = new DiffuseMaterial(new SolidColorBrush(Colors.Black));
                meshGeometry3D.TriangleIndices = int32Collection;
                model3DGroup.Children.Add(geometryModel3D);
            }


            for (int j = 7; j >= 0; j--)
            {
                for (int i = 0; i < 4; i++)
                {
                    GeometryModel3D geometryModel3D = new GeometryModel3D();
                    geometryModel3D.Material = new DiffuseMaterial()
                    {
                        Brush = StaticConstant.closeBrush
                    };
                    MeshGeometry3D meshGeometry3D = new MeshGeometry3D();
                    geometryModel3D.Geometry = meshGeometry3D;

                    Point3DCollection point3DCollection = new Point3DCollection();
                    point3DCollection.Add(new Point3D(13.5 + 2 * (i), -16.499, -7.5 + 2 * (j)));
                    point3DCollection.Add(new Point3D(12 + 2 * (i), -16.499, -7.5 + 2 * (j)));
                    point3DCollection.Add(new Point3D(12 + 2 * (i), -16, -7.5 + 2 * (j)));
                    point3DCollection.Add(new Point3D(13.5 + 2 * (i), -16, -7.5 + 2 * (j)));
                    point3DCollection.Add(new Point3D(13.5 + 2 * (i), -16, -6 + 2 * (j)));
                    point3DCollection.Add(new Point3D(13.5 + 2 * (i), -16.499, -6 + 2 * (j)));
                    point3DCollection.Add(new Point3D(12 + 2 * (i), -16.499, -6 + 2 * (j)));
                    point3DCollection.Add(new Point3D(12 + 2 * (i), -16, -6 + 2 * (j)));
                    meshGeometry3D.Positions = point3DCollection;

                    meshGeometry3D.TriangleIndices = int32Collection;
                    model3DGroup.Children.Add(geometryModel3D);
                }
            }

            for (int j = 7; j >= 0; j--)
            {
                for (int i = 4; i < 8; i++)
                {
                    GeometryModel3D geometryModel3D = new GeometryModel3D();
                    geometryModel3D.Material = new DiffuseMaterial()
                    {
                        Brush = StaticConstant.closeBrush
                    };
                    MeshGeometry3D meshGeometry3D = new MeshGeometry3D();
                    geometryModel3D.Geometry = meshGeometry3D;

                    Point3DCollection point3DCollection = new Point3DCollection();
                    point3DCollection.Add(new Point3D(13.5 + 2 * (i), -16.499, -7.5 + 2 * (j)));
                    point3DCollection.Add(new Point3D(12 + 2 * (i), -16.499, -7.5 + 2 * (j)));
                    point3DCollection.Add(new Point3D(12 + 2 * (i), -16, -7.5 + 2 * (j)));
                    point3DCollection.Add(new Point3D(13.5 + 2 * (i), -16, -7.5 + 2 * (j)));
                    point3DCollection.Add(new Point3D(13.5 + 2 * (i), -16, -6 + 2 * (j)));
                    point3DCollection.Add(new Point3D(13.5 + 2 * (i), -16.499, -6 + 2 * (j)));
                    point3DCollection.Add(new Point3D(12 + 2 * (i), -16.499, -6 + 2 * (j)));
                    point3DCollection.Add(new Point3D(12 + 2 * (i), -16, -6 + 2 * (j)));
                    meshGeometry3D.Positions = point3DCollection;

                    meshGeometry3D.TriangleIndices = int32Collection;
                    model3DGroup.Children.Add(geometryModel3D);
                }
            }

            {
                GeometryModel3D geometryModel3D = new GeometryModel3D();
                MeshGeometry3D meshGeometry3D = new MeshGeometry3D();
                geometryModel3D.Geometry = meshGeometry3D;

                Point3DCollection point3DCollection = new Point3DCollection();
                point3DCollection.Add(new Point3D(9, -18.5, 11));
                point3DCollection.Add(new Point3D(-8.5, -18.5, 11));
                point3DCollection.Add(new Point3D(-8.5, -16.5, 11));
                point3DCollection.Add(new Point3D(9, -16.5, 11));
                point3DCollection.Add(new Point3D(9, -16.5, 28));
                point3DCollection.Add(new Point3D(9, -18.5, 28));
                point3DCollection.Add(new Point3D(-8.5, -18.5, 28));
                point3DCollection.Add(new Point3D(-8.5, -16.5, 28));
                meshGeometry3D.Positions = point3DCollection;
                geometryModel3D.Material = new DiffuseMaterial(new SolidColorBrush(Colors.Black));
                meshGeometry3D.TriangleIndices = int32Collection;
                model3DGroup.Children.Add(geometryModel3D);
            }


            for (int j = 7; j >= 0; j--)
            {
                for (int i = 0; i < 4; i++)
                {
                    GeometryModel3D geometryModel3D = new GeometryModel3D();
                    geometryModel3D.Material = new DiffuseMaterial()
                    {
                        Brush = StaticConstant.closeBrush
                    };
                    MeshGeometry3D meshGeometry3D = new MeshGeometry3D();
                    geometryModel3D.Geometry = meshGeometry3D;

                    Point3DCollection point3DCollection = new Point3DCollection();
                    point3DCollection.Add(new Point3D(-6 + 2 * (i), -16.499, 12 + 2 * (j)));
                    point3DCollection.Add(new Point3D(-7.5 + 2 * (i), -16.499, 12 + 2 * (j)));
                    point3DCollection.Add(new Point3D(-7.5 + 2 * (i), -16, 12 + 2 * (j)));
                    point3DCollection.Add(new Point3D(-6 + 2 * (i), -16, 12 + 2 * (j)));
                    point3DCollection.Add(new Point3D(-6 + 2 * (i), -16, 13.5 + 2 * (j)));
                    point3DCollection.Add(new Point3D(-6 + 2 * (i), -16.499, 13.5 + 2 * (j)));
                    point3DCollection.Add(new Point3D(-7.5 + 2 * (i), -16.499, 13.5 + 2 * (j)));
                    point3DCollection.Add(new Point3D(-7.5 + 2 * (i), -16, 13.5 + 2 * (j)));
                    meshGeometry3D.Positions = point3DCollection;

                    meshGeometry3D.TriangleIndices = int32Collection;
                    model3DGroup.Children.Add(geometryModel3D);
                }
            }

            for (int j = 7; j >= 0; j--)
            {
                for (int i = 4; i < 8; i++)
                {
                    GeometryModel3D geometryModel3D = new GeometryModel3D();
                    geometryModel3D.Material = new DiffuseMaterial()
                    {
                        Brush = StaticConstant.closeBrush
                    };
                    MeshGeometry3D meshGeometry3D = new MeshGeometry3D();
                    geometryModel3D.Geometry = meshGeometry3D;

                    Point3DCollection point3DCollection = new Point3DCollection();
                    point3DCollection.Add(new Point3D(-6 + 2 * (i), -16.5, 12 + 2 * (j)));
                    point3DCollection.Add(new Point3D(-7.5 + 2 * (i), -16.5, 12 + 2 * (j)));
                    point3DCollection.Add(new Point3D(-7.5 + 2 * (i), -16, 12 + 2 * (j)));
                    point3DCollection.Add(new Point3D(-6 + 2 * (i), -16, 12 + 2 * (j)));
                    point3DCollection.Add(new Point3D(-6 + 2 * (i), -16, 13.5 + 2 * (j)));
                    point3DCollection.Add(new Point3D(-6 + 2 * (i), -16.5, 13.5 + 2 * (j)));
                    point3DCollection.Add(new Point3D(-7.5 + 2 * (i), -16.5, 13.5 + 2 * (j)));
                    point3DCollection.Add(new Point3D(-7.5 + 2 * (i), -16, 13.5 + 2 * (j)));
                    meshGeometry3D.Positions = point3DCollection;

                    meshGeometry3D.TriangleIndices = int32Collection;
                    model3DGroup.Children.Add(geometryModel3D);
                }
            }



            //(model3DGroup.Children[5] as GeometryModel3D).Material = new DiffuseMaterial()
            //{
            //    Brush = StaticConstant.brushList[5]
            //};
        }

        // Save the current image.
        private void mnuSave_Click(Object sender, RoutedEventArgs e)
        {
            // Draw the viewport into a RenderTargetBitmap.
            RenderTargetBitmap bm = new RenderTargetBitmap(
                (int)dockCube.ActualWidth, (int)dockCube.ActualHeight,
                96, 96, PixelFormats.Pbgra32);
            bm.Render(dockCube);

            // Make a PNG encoder.
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bm));

            // Save the file.
            using (FileStream fs = new FileStream("Saved.png",
                FileMode.Create, FileAccess.Write, FileShare.None))
            {
                encoder.Save(fs);
            }

            System.Media.SystemSounds.Beep.Play();
        }

        // Move the camera to the indicated position looking back at the origin.
        private void PositionCamera(float x, float y, float z, float yup)
        {
            hscroll.Value = 0;
            vscroll.Value = 0;
            PerspectiveCamera the_camera = viewCube.Camera as PerspectiveCamera;
            the_camera.Position = new Point3D(x, y, z);
            the_camera.LookDirection = new Vector3D(-x, -y, -z);
            the_camera.UpDirection = new Vector3D(0, yup, 0);

            Console.WriteLine(the_camera.Position.ToString());
            Console.WriteLine(the_camera.LookDirection.ToString());
            Console.WriteLine(the_camera.UpDirection.ToString());
            Console.WriteLine("**********");
        }

        // Move the camera to a specific position.
        private void btnView_Click(Object sender, RoutedEventArgs e)
        {
            MenuItem item = sender as MenuItem;
            string txt = item.Header.ToString().Replace("(", "").Replace(")", "");
            string[] values = txt.Split(',');
            float x = 3 * float.Parse(values[0]);
            float y = 3 * float.Parse(values[1]);
            float z = 3 * float.Parse(values[2]);
            float yup = y > 0 ? 1 : -1;
            PositionCamera(x, y, z, yup);
        }
        Int32Collection int32Collection;
        private void InitData()
        {
            int32Collection = new Int32Collection();
            int32Collection.Add(0);
            int32Collection.Add(2);
            int32Collection.Add(1);
            int32Collection.Add(0);
            int32Collection.Add(3);
            int32Collection.Add(2);
            int32Collection.Add(0);
            int32Collection.Add(4);
            int32Collection.Add(3);
            int32Collection.Add(0);
            int32Collection.Add(5);
            int32Collection.Add(4);
            int32Collection.Add(0);
            int32Collection.Add(1);
            int32Collection.Add(6);
            int32Collection.Add(0);
            int32Collection.Add(6);
            int32Collection.Add(5);
            int32Collection.Add(3);
            int32Collection.Add(4);
            int32Collection.Add(7);
            int32Collection.Add(3);
            int32Collection.Add(7);
            int32Collection.Add(2);
            int32Collection.Add(4);
            int32Collection.Add(5);
            int32Collection.Add(6);
            int32Collection.Add(4);
            int32Collection.Add(6);
            int32Collection.Add(7);
            int32Collection.Add(7);
            int32Collection.Add(6);
            int32Collection.Add(1);
            int32Collection.Add(7);
            int32Collection.Add(1);
            int32Collection.Add(2);
        }

        /// <summary>
        /// 播放
        /// </summary>
        public void Play()
        {
            NowTimePosition = 0;
            //开始播放事件
            worker.RunWorkerAsync();
        }

        /// <summary>
        /// 后台
        /// </summary>
        private BackgroundWorker worker = new BackgroundWorker();

        /// <summary>
        /// 当前节点
        /// </summary>
        private int NowTimePosition;

        /// <summary>
        /// Bpm
        /// </summary>
        private Double dWait = 96;

        /// <summary>
        /// 进度返回处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="pce"></param>
        void worker_ProgressChanged(object sender, ProgressChangedEventArgs pce)
        {
            int number = pce.ProgressPercentage;

            if (number == -1)
            {
                //清空
                //ClearAllColorExceptMembrane();
                return;
            }


            List<Model.Light> x = timeDictionary[timeList[number]];
            for (int i = 0; i < x.Count; i++)
            {
                //RoundedCornersPolygon rcp = lfe[x[i]] as RoundedCornersPolygon;
                if (x[i].Action == 128)
                {
                    int position = x[i].Position - 36;

                    if (position >= 0 && position < model3DGroup.Children.Count - 4)
                    {
                        (model3DGroup.Children[position + 4] as GeometryModel3D).Material = new DiffuseMaterial()
                        {

                            Brush = StaticConstant.closeBrush
                        };
                    }
                }
                else
                {
                    int position = x[i].Position - 36;

                    if (position >= 0 && position < model3DGroup.Children.Count - 4)
                    {
                        (model3DGroup.Children[position + 4] as GeometryModel3D).Material = new DiffuseMaterial()
                        {

                            Brush = StaticConstant.brushList[x[i].Color]
                        };
                    }
                }
            }
        }
        /// <summary>
        /// 是否在暂停状态
        /// </summary>
        private Boolean bIsPause = false;

        /// <summary>
        /// 业务逻辑处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            for (; NowTimePosition < timeList.Count; NowTimePosition++)
            {
                if (worker.CancellationPending)
                {
                    if (!bIsPause)
                    {
                        worker.ReportProgress(-1);//返回进度
                    }
                    e.Cancel = true;
                    break;
                }
                if (NowTimePosition > 0)
                {
                    Thread.Sleep(TimeSpan.FromMilliseconds(1000 / dWait * (timeList[NowTimePosition] - timeList[NowTimePosition - 1])));
                }
                worker.ReportProgress(NowTimePosition);//返回进度
            }
        }

        /// <summary>
        /// 设置数据
        /// </summary>
        /// <param name="mActionBeanList"></param>
        public void SetData(List<Model.Light> mActionBeanList)
        {
            //清空数据
            timeList.Clear();
            timeDictionary.Clear();
            //获取数据
            timeList = LightBusiness.GetTimeList(mActionBeanList);
            timeDictionary = LightBusiness.GetParagraphLightLightList(mActionBeanList);
        }

        /// <summary>
        /// 笔刷列表
        /// </summary>
        private List<SolidColorBrush> brushList = new List<SolidColorBrush>();

        /// <summary>
        /// 时间集合
        /// </summary>
        private List<int> timeList = new List<int>();

        /// <summary>
        /// 时间段落字典
        /// </summary>
        private Dictionary<int, List<Model.Light>> timeDictionary = new Dictionary<int, List<Model.Light>>();

        bool isMove = false;
        Point point = new Point(0, 0);
        private void dockOuter_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isMove = true;
            point = e.GetPosition(e.Source as FrameworkElement);
        }

        private void dockOuter_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isMove = false;
        }

        private void dockOuter_MouseLeave(object sender, MouseEventArgs e)
        {
            isMove = false;
        }

        private void dockOuter_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMove == false)
                return;

            Point _point = e.GetPosition(e.Source as FrameworkElement);
            double xMove = _point.X - point.X;
            double yMove = _point.Y - point.Y;
            if (xMove > 0)
            {
                if (Math.Abs(xMove) > ActualWidth / 180)
                {
                    point = _point;

                    if (hscroll.Value < 180)
                    {
                        hscroll.Value = hscroll.Value + 1;
                    }
                }
            }
            else
            {
                if (Math.Abs(xMove) > ActualWidth / 180)
                {
                    point = _point;
                    if (hscroll.Value > -180)
                    {
                        hscroll.Value = hscroll.Value - 1;
                    }
                }
            }
            if (yMove > 0)
            {
                if (Math.Abs(yMove) > ActualHeight / 180)
                {
                    point = _point;
                    if (vscroll.Value < 180)
                    {
                        vscroll.Value = vscroll.Value + 1;
                    }

                }
            }
            else
            {
                if (Math.Abs(yMove) > ActualHeight / 180)
                {
                    point = _point;
                    if (vscroll.Value > -180)
                    {
                        vscroll.Value = vscroll.Value - 1;
                    }
                }
            }
        }

        private void Button_Click(object sender, MouseEventArgs e)
        {
            if (double.TryParse(tbBPM.Text, out double dBpm))
            {
                dWait = dBpm;
            }
            else
            {
                dWait = 96;
            }
            Play();
        }


    
    }
}

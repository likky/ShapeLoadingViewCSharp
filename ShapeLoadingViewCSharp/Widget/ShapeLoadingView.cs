using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Util;
using Android.Graphics;

namespace ShapeLoadingViewCSharp.Widget
{
    public class ShapeLoadingView : View
    {
        public enum Shape
        {
            TRIANGLE,
            RECT,
            CIRCLE
        }

        private float genhao3 = 1.7320508075689f;
        private Shape mShape = Shape.CIRCLE;
        private float mMagicNumber = 0.55228475f;
        private Paint mPaint;
        private float mControlX = 0;
        private float mControlY = 0;
        private float mAnimPercent;
        private float triangle2Circle = 0.25555555f;

        public bool mIsLoading = false;

        public ShapeLoadingView(Context context)
            :base(context)
        {
            Init();
        }

        public ShapeLoadingView(Context context,IAttributeSet attrs)
            :base(context,attrs)
        {
            Init();
        }

        public ShapeLoadingView(Context context, IAttributeSet attrs, int defStyleAttr)
            : base(context, attrs, defStyleAttr) { }

        private void Init()
        {
            mPaint = new Paint();
            mPaint.Color = Resources.GetColor(Resource.Color.triangle);
            mPaint.AntiAlias = true;
            mPaint.SetStyle(Paint.Style.FillAndStroke);
            SetBackgroundColor(Resources.GetColor(Resource.Color.view_bg));
        }

        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);
            switch(mShape)
            {
                case Shape.TRIANGLE:
                    {
                        if (mIsLoading)
                        {
                            mAnimPercent += 0.1611113f;
                            Path path = new Path();
                            path.MoveTo(RelativeXFromView(0.5f),
                                RelativeYFromView(0.5f));

                            if (mAnimPercent >= 1)
                            {
                                mShape = Shape.CIRCLE;
                                mIsLoading = false;
                                mAnimPercent = 1;
                            }
                            float controlX = mControlX - RelativeXFromView(mAnimPercent * triangle2Circle) * genhao3;
                            float controlY = mControlY - RelativeYFromView(mAnimPercent * triangle2Circle);
                            path.QuadTo(RelativeXFromView(1) - controlX, controlY, RelativeXFromView(0.5f + genhao3 / 4),
                                RelativeYFromView(0.75f));
                            path.QuadTo(RelativeXFromView(0.5f), RelativeYFromView(0.75f + 2 * mAnimPercent * triangle2Circle),
                                RelativeXFromView(0.5f - genhao3 / 4), RelativeYFromView(0.75f));
                            path.QuadTo(controlX, controlY, RelativeXFromView(0.5f), RelativeYFromView(0f));
                            path.Close();
                            canvas.DrawPath(path, mPaint);
                            Invalidate();
                        }
                        else
                        {
                            Path path = new Path();
                            mPaint.Color = Resources.GetColor(Resource.Color.triangle);
                            path.MoveTo(RelativeXFromView(0.5f), RelativeYFromView(0f));
                            path.LineTo(RelativeXFromView(1), RelativeYFromView(genhao3 / 2f));
                            path.LineTo(RelativeXFromView(0), RelativeYFromView(genhao3 / 2f));

                            mControlX = RelativeXFromView(0.5f - genhao3 / 8f);
                            mControlY = RelativeYFromView(3 / 8f);
                            mAnimPercent = 0;
                            path.Close();
                            canvas.DrawPath(path, mPaint);
                        }
                    }
                    break;
                case Shape.CIRCLE:
                    {
                        if(mIsLoading)
                        {
                            float magicNumber = mMagicNumber + mAnimPercent;
                            mAnimPercent += 0.12f;
                            if(magicNumber + mAnimPercent > 1.9f)
                            {
                                mShape = Shape.RECT;
                                mIsLoading = false;
                            }
                            Path path = new Path();
                            path.MoveTo(RelativeXFromView(0.5f), RelativeYFromView(0f));
                            path.CubicTo(RelativeXFromView(0.5f + magicNumber / 2), RelativeYFromView(0f),
                                RelativeXFromView(1), RelativeYFromView(0.5f - magicNumber / 2),
                                RelativeXFromView(1f), RelativeYFromView(0.5f));
                            path.CubicTo(RelativeXFromView(1), RelativeXFromView(0.5f + magicNumber / 2),
                                RelativeXFromView(0.5f + mMagicNumber / 2), RelativeYFromView(1f),
                                RelativeXFromView(0.5f), RelativeYFromView(1f));
                            path.CubicTo(RelativeXFromView(0.5f - magicNumber / 2), RelativeXFromView(1f),
                                RelativeXFromView(0), RelativeYFromView(0.5f + magicNumber / 2),
                                RelativeXFromView(0f), RelativeYFromView(0.5f));
                            path.CubicTo(RelativeXFromView(0f), RelativeXFromView(0.5f - magicNumber / 2),
                                RelativeXFromView(0.5f - magicNumber / 2), RelativeYFromView(0),
                                RelativeXFromView(0.5f), RelativeYFromView(0f));
                            path.Close();
                            canvas.DrawPath(path, mPaint);
                            Invalidate();
                        }
                        else
                        {
                            mPaint.Color = Resources.GetColor(Resource.Color.circle);
                            Path path = new Path();
                            float magicNumber = mMagicNumber;
                            path.MoveTo(RelativeXFromView(0.5f), RelativeYFromView(0f));
                            path.CubicTo(RelativeXFromView(0.5f + magicNumber / 2), 0,
                                    RelativeXFromView(1), RelativeYFromView(magicNumber / 2),
                                    RelativeXFromView(1f), RelativeYFromView(0.5f));
                            path.CubicTo(
                                    RelativeXFromView(1), RelativeXFromView(0.5f + magicNumber / 2),
                                    RelativeXFromView(0.5f + magicNumber / 2), RelativeYFromView(1f),
                                    RelativeXFromView(0.5f), RelativeYFromView(1f));
                            path.CubicTo(RelativeXFromView(0.5f - magicNumber / 2), RelativeXFromView(1f),
                                    RelativeXFromView(0), RelativeYFromView(0.5f + magicNumber / 2),
                                    RelativeXFromView(0f), RelativeYFromView(0.5f));
                            path.CubicTo(RelativeXFromView(0f), RelativeXFromView(0.5f - magicNumber / 2),
                                    RelativeXFromView(0.5f - magicNumber / 2), RelativeYFromView(0),
                                    RelativeXFromView(0.5f), RelativeYFromView(0f));
                            mAnimPercent = 0;
                            path.Close();
                            canvas.DrawPath(path, mPaint);
                        }
                    }
                    break;
                case Shape.RECT:
                    {
                        if(mIsLoading)
                        {
                            mAnimPercent += 0.15f;
                            if(mAnimPercent >= 1)
                            {
                                mShape = Shape.TRIANGLE;
                                mIsLoading = false;
                                mAnimPercent = 1;
                            }
                            Path path = new Path();
                            path.MoveTo(RelativeXFromView(0.5f * mAnimPercent), 0);
                            path.LineTo(RelativeYFromView(1 - 0.5f * mAnimPercent), 0);
                            float distanceX = (mControlX) * mAnimPercent;
                            float distanceY = (RelativeYFromView(1f) - mControlY) * mAnimPercent;
                            path.LineTo(RelativeXFromView(1f) - distanceX, RelativeYFromView(1f) - distanceY);
                            path.LineTo(RelativeXFromView(0f) + distanceX, RelativeYFromView(1f) - distanceY);
                            path.Close();
                            canvas.DrawPath(path, mPaint);
                            Invalidate();
                        }
                        else
                        {
                            mPaint.Color = Resources.GetColor(Resource.Color.rect);
                            mControlX = RelativeXFromView(0.5f - genhao3 / 4);
                            mControlY = RelativeYFromView(0.75f);
                            Path path = new Path();
                            path.MoveTo(RelativeXFromView(0f), RelativeYFromView(0f));
                            path.LineTo(RelativeXFromView(1f), RelativeYFromView(0f));
                            path.LineTo(RelativeXFromView(1f), RelativeYFromView(1f));
                            path.LineTo(RelativeXFromView(0f), RelativeYFromView(1f));
                            path.Close();
                            mAnimPercent = 0;
                            canvas.DrawPath(path, mPaint);
                        }
                    }
                    break;
            }
        }

        private float RelativeYFromView(float p)
        {
            return Height * p;
        }

        private float RelativeXFromView(float p)
        {
            return Width * p;
        }

        public void ChangeShape()
        {
            mIsLoading = true;
            Invalidate();
        }

        public Shape GetShape()
        {
            return mShape;
        }
    }
}
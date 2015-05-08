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
using Android.Content.Res;
using Android.Animation;
using Android.Views.Animations;

namespace ShapeLoadingViewCSharp.Widget
{
    public class LoadingView : FrameLayout
    {
        private ShapeLoadingView shapeLoadingView;
        private ImageView indicationIm;
        private TextView loadTextView;
        private static int ANIMATION_DURATION = 500;
        private string loadText;
        private float mDistance = 200;

        public LoadingView(Context context)
            : base(context) { }

        public LoadingView(Context context,IAttributeSet attrs)
            :base(context,attrs)
        {
            Init(context, attrs);
        }

        public LoadingView(Context context,IAttributeSet attrs,int defStyleAttr)
            :base(context,attrs,defStyleAttr)
        {
            Init(context, attrs);
        }

        public int Dip2Px(float dipValue)
        {
            float scale = Context.Resources.DisplayMetrics.Density;
            return (int)(dipValue * scale + 0.5f);
        }

        protected override void OnFinishInflate()
        {
            base.OnFinishInflate();
            View view = LayoutInflater.From(Context).Inflate(Resource.Layout.LoadView, null);
            mDistance = Dip2Px(54f);
            LayoutParams layoutParams = new LayoutParams(ViewGroup.LayoutParams.WrapContent,
                ViewGroup.LayoutParams.WrapContent);
            layoutParams.Gravity = GravityFlags.Center;
            shapeLoadingView = view.FindViewById<ShapeLoadingView>(Resource.Id.shapeLoadingView);
            indicationIm = view.FindViewById<ImageView>(Resource.Id.indication);
            loadTextView = view.FindViewById<TextView>(Resource.Id.promptTV);
            SetLoadingText(loadText);
            AddView(view, layoutParams);
            this.PostDelayed(() =>
            {
                FreeFall();
            }, 900);
        }

        public void UpThrow()
        {
            ObjectAnimator objectAnimator = ObjectAnimator.OfFloat(shapeLoadingView, "translationY", mDistance, 0);
            ObjectAnimator scaleIndication = ObjectAnimator.OfFloat(indicationIm, "scaleX", 0.2f, 1);

            ObjectAnimator objectAnimator1 = null;
            switch(shapeLoadingView.GetShape())
            {
                case ShapeLoadingView.Shape.RECT:
                    {
                        objectAnimator1 = ObjectAnimator.OfFloat(shapeLoadingView, "rotation", 0, -120);
                    }
                    break;
                case ShapeLoadingView.Shape.CIRCLE:
                    {
                        objectAnimator1 = ObjectAnimator.OfFloat(shapeLoadingView, "rotation", 0, 180);
                    }
                    break;
                case ShapeLoadingView.Shape.TRIANGLE:
                    {
                        objectAnimator1 = ObjectAnimator.OfFloat(shapeLoadingView, "rotation", 0, 180);
                    }
                    break;
            }
            objectAnimator.SetDuration(ANIMATION_DURATION);
            objectAnimator1.SetDuration(ANIMATION_DURATION);
            objectAnimator.SetInterpolator(new DecelerateInterpolator());
            objectAnimator1.SetInterpolator(new DecelerateInterpolator());
            AnimatorSet animatorSet = new AnimatorSet();
            animatorSet.SetDuration(ANIMATION_DURATION);
            animatorSet.PlayTogether(objectAnimator, objectAnimator1, scaleIndication);
            animatorSet.AnimationEnd += (e, s) =>
            {
                FreeFall();
            };
            animatorSet.Start();
        }

        public void FreeFall()
        {
            ObjectAnimator objectAnimator = ObjectAnimator.OfFloat(shapeLoadingView, "translationY", 0, mDistance);
            ObjectAnimator scaleIndication = ObjectAnimator.OfFloat(indicationIm, "scaleX", 1, 0.2f);

            objectAnimator.SetDuration(ANIMATION_DURATION);
            objectAnimator.SetInterpolator(new AccelerateInterpolator());
            AnimatorSet animatorSet = new AnimatorSet();
            animatorSet.SetDuration(ANIMATION_DURATION);
            animatorSet.PlayTogether(objectAnimator, scaleIndication);
            animatorSet.AnimationEnd += (e, s) =>
            {
                shapeLoadingView.ChangeShape();
                UpThrow();
            };
            animatorSet.Start();
        }

        public void SetLoadingText(string loadText)
        {
            loadTextView.Text = loadText;
        }

        private void Init(Android.Content.Context context, IAttributeSet attrs)
        {
            TypedArray typedArray = context.ObtainStyledAttributes(attrs, Resource.Styleable.LoadingView);
            loadText = typedArray.GetString(Resource.Styleable.LoadingView_loadingText);
            typedArray.Recycle();
        }
    }
}
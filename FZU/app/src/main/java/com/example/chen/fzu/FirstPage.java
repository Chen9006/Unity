package com.example.chen.fzu;

import android.content.Context;
import android.graphics.Color;
import android.graphics.drawable.Drawable;
import android.net.Uri;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentManager;
import android.support.v4.app.FragmentPagerAdapter;
import android.support.v4.app.FragmentTransaction;
import android.support.v4.view.ViewPager;
import android.util.DisplayMetrics;
import android.util.TypedValue;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewConfiguration;
import android.view.ViewGroup;
import android.widget.FrameLayout;
import android.widget.RadioButton;
import android.widget.RadioGroup;
import android.widget.TextView;

import com.astuetz.PagerSlidingTabStrip;

import java.lang.reflect.Field;
import java.util.ArrayList;
import java.util.List;


public class FirstPage extends Fragment implements View.OnClickListener {
    /**
     * 定义一个布局
     */
    private LayoutInflater inflater;
    //  private View rootView;// 缓存Fragment view
    private Context mainActivity;
    private RealTime RT;
    private Hot_Topic HT;
    private MyAcademy MA;
    /**
     * one、two RadioGroup 控件
     */
    private RadioGroup radioGroup;
    protected RadioButton RT_Button, HT_bUtton, MA_button;

    /**
     * 加载页面
     */
    @Override
    public void onActivityCreated(Bundle savedInstanceState) {
        super.onActivityCreated(savedInstanceState);
        mainActivity = getActivity();
        inflater = LayoutInflater.from(getActivity());
//      初始化控件和声明事件
//        rootView = inflater.inflate(R.layout.two, null);
        radioGroup = (RadioGroup) getActivity().findViewById(R.id.first_page_radioGroup);
        RT_Button = (RadioButton) getActivity().findViewById(R.id.real_time_button);
        HT_bUtton = (RadioButton) getActivity().findViewById(R.id.hot_button);
        MA_button = (RadioButton) getActivity().findViewById(R.id.myacademy_button);
        //控件颜色
//        twoOne.setTextColor(getResources().getColor(R.color.red));
//        twoTwo.setTextColor(getResources().getColor(R.color.black));



        RT_Button.setOnClickListener(this);
        HT_bUtton.setOnClickListener(this);
        MA_button.setOnClickListener(this);

        setDefaultFragment();
    }

    /**
     * 设置默认的Fragment
     */
    private void setDefaultFragment() {
        FragmentManager fm = getFragmentManager();
        FragmentTransaction transaction = fm.beginTransaction();
        RT = new RealTime();
        transaction.add(R.id.first_page_fragment, RT).commit();
    }

    @Override
    public void onClick(View v) {
        FragmentManager fm = getFragmentManager();
        // 开启Fragment事务
        FragmentTransaction transaction = fm.beginTransaction();

        switch (v.getId()) {
            case R.id.real_time_button:
                if (RT == null) {
                    RT = new RealTime();
                }
                // 使用当前Fragment的布局替代id_content的控件
                transaction.replace(R.id.first_page_fragment, RT);
                //控件颜色
//                twoOne.setTextColor(getResources().getColor(R.color.red));
//                twoTwo.setTextColor(getResources().getColor(R.color.black));
                break;
            case R.id.hot_button:
                if (HT == null) {
                    HT = new Hot_Topic();
                }
                transaction.replace(R.id.first_page_fragment, HT);
                //控件颜色
//                twoOne.setTextColor(getResources().getColor(R.color.black));
//                twoTwo.setTextColor(getResources().getColor(R.color.red));
                break;

            case R.id.myacademy_button:
                if (MA == null) {
                    MA = new MyAcademy();
                }
                transaction.replace(R.id.first_page_fragment, MA);
                //控件颜色
//                twoOne.setTextColor(getResources().getColor(R.color.black));
//                twoTwo.setTextColor(getResources().getColor(R.color.red));
                break;

        }
        // transaction.addToBackStack();
        // 事务提交
        transaction.commit();
    }


    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        return inflater.inflate(R.layout.fragment_first_page, null);

    }


}

//    private View mView;
//
//    @Nullable
//    @Override
//    public View onCreateView(LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
//        //注意View对象的重复使用，以便节省资源
//        if (mView == null) {
//            mView = inflater.inflate(R.layout.fragment_first_page,container, false);
//        }
//
//        return mView;
//    }

/**
 * PagerSlidingTabStrip的实例
 */
//    private PagerSlidingTabStrip tabs;
//
//    /**
//     * 获取当前屏幕的密度
//     */
//    private DisplayMetrics dm;
//
//    private RealTime oneFragment;
//    private Hot_Topic twoFragment;
//    private MyAcademy threeFragment;
//    /*
//     *
//     */
//
//    @Override
//    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
//        // TODO Auto-generated method stub
//        View view = inflater.inflate(R.layout.fragment_first_page, null);
//        setOverflowShowingAlways();
//        dm = getResources().getDisplayMetrics();
//        ViewPager pager = (ViewPager) view.findViewById(R.id.pager);
//        pager.setOffscreenPageLimit(0);//设置ViewPager的缓存界面数,默认缓存为2
//        tabs = (PagerSlidingTabStrip) view.findViewById(R.id.tabs);
//        pager.setAdapter(new MyPagerAdapter(getChildFragmentManager()));
//       // setTabsValue();
//        tabs.setViewPager(pager);
//
//        return view;
//    }
//
//
//    /**
//     * 对PagerSlidingTabStrip的各项属性进行赋值。
//     */
//    private void setTabsValue() {
//        // 设置Tab是自动填充满屏幕的
//        tabs.setShouldExpand(true);
//        // 设置Tab的分割线是透明的
//        tabs.setDividerColor(Color.TRANSPARENT);
//        // 设置Tab底部线的高度
//        tabs.setUnderlineHeight((int) TypedValue.applyDimension(TypedValue.COMPLEX_UNIT_DIP, 1, dm));
//        // 设置Tab Indicator的高度
//        tabs.setIndicatorHeight((int) TypedValue.applyDimension(TypedValue.COMPLEX_UNIT_DIP, 3, dm));
//        // 设置Tab标题文字的大小
//        tabs.setTextSize((int) TypedValue.applyDimension(TypedValue.COMPLEX_UNIT_SP, 16, dm));
//        // 设置Tab Indicator的颜色
//        tabs.setIndicatorColor(Color.parseColor("#d83737"));//#d83737   #d83737(绿)
//        // 设置选中Tab文字的颜色 (这是我自定义的一个方法)
//        //  tabs.setSelectedTextColor(Color.parseColor("#ffffff"));
//        // 取消点击Tab时的背景色
//        tabs.setTabBackground(0);
//    }
//
//    public class MyPagerAdapter extends FragmentPagerAdapter {
//
//        public MyPagerAdapter(FragmentManager fm) {
//            super(fm);
//        }
//
//        private final String[] titles = {"123", "456", "789"};
//
//        @Override
//        public CharSequence getPageTitle(int position) {
//            return titles[position];
//        }
//
//        @Override
//        public int getCount() {
//            return titles.length;
//        }
//
//        @Override
//        public Fragment getItem(int position) {
//            switch (position) {
//                case 0:
//                    if (oneFragment == null) {
//                        oneFragment = new RealTime();
//                    }
//                    return oneFragment;
//                case 1:
//                    if (twoFragment == null) {
//                        twoFragment = new Hot_Topic();
//                    }
//                    return twoFragment;
//                case 2:
//                    if (threeFragment == null) {
//                        threeFragment = new MyAcademy();
//                    }
//                    return threeFragment;
//                default:
//                    return null;
//            }
//        }
//
//    }
//
//    private void setOverflowShowingAlways() {
//        try {
//            ViewConfiguration config = ViewConfiguration.get(getParentFragment().getActivity());
//            Field menuKeyField = ViewConfiguration.class
//                    .getDeclaredField("sHasPermanentMenuKey");
//            menuKeyField.setAccessible(true);
//            menuKeyField.setBoolean(config, false);
//        } catch (Exception e) {
//            e.printStackTrace();
//        }
//    }






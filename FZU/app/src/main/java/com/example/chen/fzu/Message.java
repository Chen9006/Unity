package com.example.chen.fzu;

import android.content.Context;
import android.net.Uri;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentActivity;
import android.support.v4.app.FragmentManager;
import android.support.v4.app.FragmentTransaction;
import android.support.v7.app.AppCompatActivity;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.FrameLayout;
import android.widget.RadioButton;
import android.widget.RadioGroup;

import java.util.ArrayList;
import java.util.List;


/**
 * A simple {@link Fragment} subclass.
 * Activities that contain this fragment must implement the
 * {@link Message.OnFragmentInteractionListener} interface
 * to handle interaction events.
 * Use the {@link Message#newInstance} factory method to
 * create an instance of this fragment.
 */
public class Message extends Fragment  {
    private View mView;

    @Nullable
    @Override
    public View onCreateView(LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        //注意View对象的重复使用，以便节省资源
        if (mView == null) {
            mView = inflater.inflate(R.layout.fragment_message, container, false);
        }

        return mView;
    }
}
//
//
//    private RadioGroup radioGroup;
//    private RadioButton button_1;
//    private RadioButton button_2;
//    private RadioButton button_3;
//    //private RadioButton button_4;
//    private RealTime fragment_1;
//    //    private Question_Answer fragment_2;
//    //   private Message fragment_3;
//    //  private Fragment_4 fragment_4;
//    private List<Fragment> list;
//    private FrameLayout frameLayout;
//
//
//
//    //初始化页面
//    private void initView() {
//
//    }
//
////    @Override
////    public void finish() {
////        ViewGroup viewGroup = (ViewGroup) getWindow().getDecorView();
////        viewGroup.removeAllViews();
////        super.finish();
////    }
//
//
//    private View mView;
//
//    @Nullable
//    @Override
//    public View onCreateView(LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
//        //注意View对象的重复使用，以便节省资源
//
//        frameLayout = (FrameLayout) getView().findViewById(R.id.first_page_framelayout);
//        radioGroup = (RadioGroup) getView().findViewById(R.id.first_page_radioGroup);
//
//        //找到四个按钮
//        button_1 = (RadioButton) getView().findViewById(R.id.real_time_button);
//        //  button_2 = (RadioButton) getView().findViewById(R.id.button_2);
//        //   button_3 = (RadioButton) getView().findViewById(R.id.button_3);
//        // button_4 = (RadioButton) findViewById(R.id.button_4);
////
////        Drawable drawable_news = getResources().getDrawable(R.drawable.homepage);
////        //当这个图片被绘制时，给他绑定一个矩形 ltrb规定这个矩形
////        drawable_news.setBounds(0, 0, 80, 80);
////        //设置图片在文字的哪个方向
////        button_1.setCompoundDrawables(null, drawable_news, null, null);
////
////        //定义底部标签图片大小和位置
////        Drawable drawable_live = getResources().getDrawable(R.drawable.question);
////        //当这个图片被绘制时，给他绑定一个矩形 ltrb规定这个矩形
////        drawable_live.setBounds(0, 0, 80, 80);
////        //设置图片在文字的哪个方向
////        button_2.setCompoundDrawables(null, drawable_live, null, null);
////
////        //定义底部标签图片大小和位置
////        Drawable drawable_tuijian = getResources().getDrawable(R.drawable.message);
////        //当这个图片被绘制时，给他绑定一个矩形 ltrb规定这个矩形
////        drawable_tuijian.setBounds(0, 0, 80, 80);
////        //设置图片在文字的哪个方向
////        button_3.setCompoundDrawables(null, drawable_tuijian, null, null);
//
//
//
//        //创建Fragment对象及集合
//        fragment_1 = new RealTime();
//        //   fragment_2 = new Question_Answer();
//        //   fragment_3 = new Message();
//        // fragment_4 = new Fragment_4();
//
//        //将Fragment对象添加到list中
//        list = new ArrayList<>();
//        list.add(fragment_1);
//        //   list.add(fragment_2);
//        //  list.add(fragment_3);
//        // list.add(fragment_4);
//
//        //设置RadioGroup开始时设置的按钮，设置第一个按钮为默认值
//        radioGroup.check(R.id.button_1);
//
//
//        //设置按钮点击监听
//        button_1.setOnClickListener(this);
//        //   button_2.setOnClickListener(this);
//        //   button_3.setOnClickListener(this);
//        //button_4.setOnClickListener(this);
//
//        //初始时向容器中添加第一个Fragment对象
//        addFragment(fragment_1);
//
//        if (mView == null) {
//            mView = inflater.inflate(R.layout.fragment_first_page,container, false);
//        }
//
//
//
//        return mView;
//
//    }
//
//    //点击事件处理
//    @Override
//    public void onClick(View v) {
//        //我们根据参数的id区别不同按钮
//        //不同按钮对应着不同的Fragment对象页面
//        switch (v.getId()) {
//            case R.id.button_1:
//                addFragment(fragment_1);
//                break;
////            case R.id.button_2:
////                addFragment(fragment_2);
////                break;
////            case R.id.button_3:
////                addFragment(fragment_3);
////                break;
////            case R.id.button_4:
////                addFragment(fragment_4);
////                break;
//            default:
//                break;
//        }
//
//    }
//
//    //向Activity中添加Fragment的方法
//    public void addFragment(Fragment fragment) {
//
//        //获得Fragment管理器
//        FragmentManager fragmentManager = getFragmentManager();
//        //使用管理器开启事务
//        FragmentTransaction fragmentTransaction = getChildFragmentManager().beginTransaction();
//        //使用事务替换Fragment容器中Fragment对象
//        fragmentTransaction.replace(R.id.first_page_framelayout,fragment);
//        //提交事务，否则事务不生效
//        fragmentTransaction.commit();
//    }
//
//
//}

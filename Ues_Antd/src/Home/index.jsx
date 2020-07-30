/* eslint no-undef: 0 */
/* eslint arrow-parens: 0 */
import React from 'react';
import { enquireScreen } from 'enquire-js';
// import logo from '../statics/logo.svg'
// import uPic from '../statics/U-01.svg'


import Nav2 from './Nav2';
import Banner5 from './Banner5';
// import Feature0 from './Feature0';
// import Pricing0 from './Pricing0';
// import Feature3 from './Feature3';
// import Feature1 from './Feature1';
// import Feature2 from './Feature2';
import Content5 from './Content5';
// import Feature7 from './Feature7';
import Footer0 from './Footer0';

import {
  Nav20DataSource,
  Banner50DataSource,
  // Feature00DataSource,
  // Pricing00DataSource,
  // Feature30DataSource,
  // Feature11DataSource,
  // Feature20DataSource,
  Content50DataSource,
  // Feature70DataSource,
  Footer00DataSource,
} from './data.source';
import './less/antMotionStyle.less';
import * as planApi from '../repository/planRepo';

let isMobile;
enquireScreen((b) => {
  isMobile = b;
});

const { location = {} } = typeof window !== 'undefined' ? window : {};

export default class Home extends React.Component {

  constructor(props) {
    console.log(document.cookie);
    super(props);
    this.state = {
      isMobile,
      show: !location.port, // 如果不是 dva 2.0 请删除
      cd: Content50DataSource
    };
  }

  componentDidMount() {
    // // 适配手机屏幕;
    // enquireScreen((b) => {
    //   this.setState({ isMobile: !!b });
    // });
    // // dva 2.0 样式在组件渲染之后动态加载，导致滚动组件不生效；线上不影响；
    // /* 如果不是 dva 2.0 请删除 start */
    // if (location.port) {
    //   // 样式 build 时间在 200-300ms 之间;
    //   setTimeout(() => {
    //     this.setState({
    //       show: true,
    //     });
    //   }, 500);
    // }
    this.setState({
      show: false
    })
    planApi.getPlan().then((plans) => {
      Content50DataSource.block.children = [];
      let i = 0;
      plans.forEach(e => {
        Content50DataSource.block.children.push(
          {
            md: 8,
            xs: 24,
            name: 'b' + i,
            className: 'feature7-block',
            children: {
              className: 'feature7-block-group',
              children: [
                {
                  name: 'title',
                  className: 'feature7-block-title',
                  children: e.Title
                },
                {
                  name: 'content',
                  className: 'feature7-block-content',
                  children: e.Description
                },
              ],
            },
          },
          // {
          //   name: 'b' + i,
          //   className: 'block',
          //   md: 6,
          //   xs: 24,
          //   children: {
          //     wrapper: { className: 'content5-block-content' },
          //     img: {
          //       children: e.PlanCategory === 'A' ? tPic : uPic,
          //       back: e.PlanCategory === 'A' ? "#e9e9e9" : "#001529"
          //     },
          //     content: { children: e.Title },
          //   },
          // },
        );
        i++;
      });
      this.setState({
        cd: Content50DataSource,
        show: true
      })
      // document.getElementById("modalE").click();
      // document.getElementById("modalC").click();
    });
    /* 如果不是 dva 2.0 请删除 end */
  }



  render() {
    const children = [
      <Nav2
        id="Nav2_0"
        key="Nav2_0"
        dataSource={Nav20DataSource}
        isMobile={this.state.isMobile}
      />,
      <Banner5
        id="Banner5_0"
        key="Banner5_0"
        dataSource={Banner50DataSource}
        isMobile={this.state.isMobile}
      />,
      // <Feature0
      //   id="Feature0_0"
      //   key="Feature0_0"
      //   dataSource={Feature00DataSource}
      //   isMobile={this.state.isMobile}
      // />,
      // <Pricing0
      //   id="Pricing0_0"
      //   key="Pricing0_0"
      //   dataSource={Pricing00DataSource}
      //   isMobile={this.state.isMobile}
      // />,
      // <Feature3
      //   id="Feature3_0"
      //   key="Feature3_0"
      //   dataSource={Feature30DataSource}
      //   isMobile={this.state.isMobile}
      // />,
      // <Feature1
      //   id="Feature1_1"
      //   key="Feature1_1"
      //   dataSource={Feature11DataSource}
      //   isMobile={this.state.isMobile}
      // />,
      // <Feature2
      //   id="Feature2_0"
      //   key="Feature2_0"
      //   dataSource={Feature20DataSource}
      //   isMobile={this.state.isMobile}
      // />,
      <Content5
        id="Content5_0"
        key="Content5_0"
        dataSource={this.state.cd}
        isMobile={this.state.isMobile}
      />,
      // <Feature7
      //   id="Feature7_0"
      //   key="Feature7_0"
      //   dataSource={Feature70DataSource}
      //   isMobile={this.state.isMobile}
      // />,
      <Footer0
        id="Footer0_0"
        key="Footer0_0"
        dataSource={Footer00DataSource}
        isMobile={this.state.isMobile}
      />,
    ];
    return (
      <div
        className="templates-wrapper"
        ref={(d) => {
          this.dom = d;
        }}
      >
        {/* 如果不是 dva 2.0 替换成 {children} start */}
        {this.state.show && children}
        {/* 如果不是 dva 2.0 替换成 {children} end */}
      </div>
    );
  }
}

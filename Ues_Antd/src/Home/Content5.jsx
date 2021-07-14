import React from 'react';
import { Row, Col } from 'antd';
import { TweenOneGroup } from 'rc-tween-one';
import OverPack from 'rc-scroll-anim/lib/ScrollOverPack';
import { getChildrenToRender } from './utils';
import { Modal, Button, Input, message } from 'antd';
import * as adminApi from '../repository/adminRepo'

class Content5 extends React.PureComponent {

  constructor(props) {
    super(props);
    this.state = {
      loginVisible: false,
      email: "",
      pwd: ""
    };
  }

  validateInfo(loginflag) {
    if (this.state.email === "" || this.state.email == null || this.state.pwd === "" || this.state.pwd == null) {
      message.warning("请填写完整");
      return false;
    }
    var myReg = /^[a-zA-Z0-9_-]+@([a-zA-Z0-9]+\.)+(com|cn|net|org)$/;
    if (!myReg.test(this.state.email) && this.state.email !== "admin") {
      message.warning("邮箱格式不正确");
      return false;
    }
    if (this.state.pwd.length < 6 && !loginflag) {
      message.warning("请保证密码不小于6位");
      return false;
    }
    return true;
  }

  login() {
    if (this.validateInfo(true)) {
      adminApi.LoginSumbit(this.state.email, this.state.pwd).then(x => {
        if (x === "overDate") {
          message.warning("账号已过期，请联系管理员");
        }
        else if (x) {
          window.location.href = window.location.origin + "/Home/DashBoard"
        }
        else {
          message.error("账号或密码错误，请重新输入");
        }
      });

    }
  }

  forget() {
    var myReg = /^[a-zA-Z0-9_-]+@([a-zA-Z0-9]+\.)+(com|cn|net|org)$/;
    if (!myReg.test(this.state.email) && this.state.email !== "admin") {
      message.warning("邮箱格式不正确");
      return false;
    }
    adminApi.ReSetSumbit(this.state.email, this.state.pwd).then(x => {
      if (x === true) {
        message.info("重置密码邮件已发送，请检查邮箱！");
      }
      else {
        message.warning(x);
      }
    });
  }

  register() {
    if (this.validateInfo(false)) {
      adminApi.RgisterSumbit(this.state.email, this.state.pwd).then(x => {
        if (x === "UserExist") {
          message.warning("邮箱已注册，请联系管理员");
        }
        else if (x) {
          window.location.href = window.location.origin + "/Home/DashBoard"
        }
        else {
          message.warning("注册失败，请联系管理员");
        }
      });

    }
  }

  show() {
    this.setState({
      loginVisible: true
    })
  }

  close() {
    this.setState({
      loginVisible: false
    })
  }

  getChildrenToRender = (data) =>
    data.map((item) => {
      return (
        <Col {...item} key={item.name} onClick={(e) => {
          this.show();
        }}>
          <a {...item.children}>
            {item.children.children.map(getChildrenToRender)}
          </a>
        </Col>
      );
      // return (
      //   <Col key={item.name} {...item} onClick={(e) => {
      //     this.show();
      //   }}>
      //     <div {...item.children.wrapper}>
      //       <p style={{ fontWeight: 'bold' }} {...item.children.content}>{item.children.content.children}</p>
      //       <p style={{ fontWeight: 'bold' }} {...item.children.content}>{item.children.content.children}</p>
      //       {/* <span {...item.children.img} style={{ background: item.children.img.back }}>
      //         <img src={item.children.img.children} height="90%" alt="img" />           
      //       </span>          */}
      //     </div>
      //   </Col>
      // );
    });

  render() {
    const { ...props } = this.props;
    const { dataSource } = props;
    delete props.dataSource;
    delete props.isMobile;
    const childrenToRender = this.getChildrenToRender(
      dataSource.block.children
    );
    return (
      <div {...props} {...dataSource.wrapper}>
        <div {...dataSource.page}>
          <div key="title" {...dataSource.titleWrapper}>
            {dataSource.titleWrapper.children.map(getChildrenToRender)}
          </div>
          <OverPack
            className={`content-template ${props.className}`}
            {...dataSource.OverPack}
          >
            <TweenOneGroup
              component={Row}
              key="ul"
              enter={{
                y: '+=30',
                opacity: 0,
                type: 'from',
                ease: 'easeInOutQuad',
              }}
              leave={{ y: '+=30', opacity: 0, ease: 'easeInOutQuad' }}
              {...dataSource.block}
            >
              {childrenToRender}
            </TweenOneGroup>
          </OverPack>
        </div>
        <Modal
          title={"欢迎登录UES"}
          visible={this.state.loginVisible}
          width="600px"
          maskClosable={false}
          closable={false}
          footer={[
            <Button type="primary" onClick={() => {
              this.login();
            }}>登录</Button>,
            <Button type="primary" onClick={() => {
              this.register();
            }}>注册</Button>,
            <Button type="primary" onClick={() => {
              this.forget();
            }}>忘记密码</Button>,
            <Button onClick={() => {
              this.close();
            }}>取消</Button>]}
        >
          <Input addonBefore="邮箱" placeholder="请设置" value={this.state.email} onChange={e => {
            this.setState({
              email: e.target.value
            })
          }} type="email" />
          <br />
          <br />
          <Input addonBefore="密码" placeholder="请设置" value={this.state.pwd} onChange={e => {
            this.setState({
              pwd: e.target.value
            })
          }} type="password" />
        </Modal>
        <div id="modalE" onClick={() => {
          this.show();
        }}></div>
        <div id="modalC" onClick={() => {
          this.close();
        }}></div>
      </div>
    );
  }
}

export default Content5;

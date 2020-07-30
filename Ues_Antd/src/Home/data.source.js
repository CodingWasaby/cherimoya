import React from 'react';
export const Nav20DataSource = {
  isScrollLink: true,
  wrapper: { className: 'header2 home-page-wrapper' },
  page: { className: 'home-page' },
  logo: {
    className: 'header2-logo',
    children: 'https://os.alipayobjects.com/rmsportal/mlcYmsRilwraoAe.svg',
    //  children: '/favicon.ico',
  },
  LinkMenu: {
    className: 'header2-menu',
    children: [
      {
        name: 'linkNav',
        to: 'Content5_0',
        children: '模型',
        className: 'menu-item',
        id: 'abc'
      },
      {
        name: 'linkNav',
        children: '登录',
        className: 'menu-item',
        id: 'bcd',
        onClick: () => {
           document.getElementById("modalE").click();
        }
      },
    ],
  },
  mobileMenu: { className: 'header2-mobile-menu' },
};
export const Banner50DataSource = {
  wrapper: { className: 'home-page-wrapper banner5' },
  page: { className: 'home-page banner5-page' },
  childWrapper: {
    className: 'banner5-title-wrapper',
    children: [
      { name: 'title', children: 'Uncertainty Evaluation System', className: 'banner5-title' },
      {
        name: 'explain',
        className: 'banner5-explain',
        children: '不确定度评估分析系统',
      },
      {
        name: 'content',
        className: 'banner5-content',
        children: 'UES 提供方便，快捷，可持久化的各种不确定度计算功能。\n你可以自定义实验过程，也可以使用别人已经创建好的实验模板进行实验。',
      },
      {
        name: 'button',
        className: 'banner5-button-wrapper',
        children: {
          className: 'banner5-button',
          type: 'primary',
          children: '浏览模型',
          onClick: () => {
            document.getElementById('abc').click();
          }
        },
      }
    ],
  },
  image: {
    className: 'banner5-image',
    children:
      'https://gw.alipayobjects.com/mdn/rms_ae7ad9/afts/img/A*-wAhRYnWQscAAAAAAAAAAABkARQnAQ',
  },
};
export const Feature00DataSource = {
  wrapper: { className: 'home-page-wrapper content0-wrapper' },
  page: { className: 'home-page content0' },
  OverPack: { playScale: 0.3, className: '' },
  titleWrapper: {
    className: 'title-wrapper',
    children: [{ name: 'title', children: '产品与服务' }],
  },
  childWrapper: {
    className: 'content0-block-wrapper',
    children: [
      {
        name: 'block0',
        className: 'content0-block',
        md: 8,
        xs: 24,
        children: {
          className: 'content0-block-item',
          children: [
            {
              name: 'image',
              className: 'content0-block-icon',
              children:
                'https://zos.alipayobjects.com/rmsportal/WBnVOjtIlGWbzyQivuyq.png',
            },
            {
              name: 'title',
              className: 'content0-block-title',
              children: '一站式业务接入',
            },
            { name: 'content', children: '支付、结算、核算接入产品效率翻四倍' },
          ],
        },
      },
      {
        name: 'block1',
        className: 'content0-block',
        md: 8,
        xs: 24,
        children: {
          className: 'content0-block-item',
          children: [
            {
              name: 'image',
              className: 'content0-block-icon',
              children:
                'https://zos.alipayobjects.com/rmsportal/YPMsLQuCEXtuEkmXTTdk.png',
            },
            {
              name: 'title',
              className: 'content0-block-title',
              children: '一站式事中风险监控',
            },
            {
              name: 'content',
              children: '在所有需求配置环节事前风险控制和质量控制能力',
            },
          ],
        },
      },
      {
        name: 'block2',
        className: 'content0-block',
        md: 8,
        xs: 24,
        children: {
          className: 'content0-block-item',
          children: [
            {
              name: 'image',
              className: 'content0-block-icon',
              children:
                'https://zos.alipayobjects.com/rmsportal/EkXWVvAaFJKCzhMmQYiX.png',
            },
            {
              name: 'title',
              className: 'content0-block-title',
              children: '一站式数据运营',
            },
            {
              name: 'content',
              children: '沉淀产品接入效率和运营小二工作效率数据',
            },
          ],
        },
      },
    ],
  },
};
export const Pricing00DataSource = {
  wrapper: { className: 'home-page-wrapper pricing0-wrapper' },
  OverPack: { playScale: 0.3, className: 'home-page pricing0' },
  imgWrapper: { className: 'pricing0-img-wrapper', md: 12, xs: 24 },
  img: {
    className: 'pricing0-img',
    name: 'image',
    children:
      'https://gw.alipayobjects.com/mdn/rms_ae7ad9/afts/img/A*OnyWT4Nsxy0AAAAAAAAAAABjARQnAQ',
  },
  childWrapper: {
    className: 'pricing0-text-wrapper',
    md: 12,
    xs: 24,
    children: [
      {
        name: 'title',
        children: 'OceanBase 服务器',
        className: 'pricing0-title',
      },
      {
        name: 'content',
        children:
          '云资源集中编排、弹性伸缩、持续发布和部署，高可用及容灾。按金融企业安全要求打造的完整云上安全体系，全方位保障金融应用及数据安全。<br/>500-5Gbps，10 GB-50TB（含），1TB流量包，国内按峰值。',
        className: 'pricing0-content',
      },
      { name: 'pricing', children: '¥2,200', className: 'pricing0-pricing' },
      {
        name: 'button',
        children: {
          icon: 'shopping-cart',
          href: '#',
          type: 'primary',
          children: '立即购买',
        },
      },
    ],
  },
};
export const Feature30DataSource = {
  wrapper: { className: 'home-page-wrapper content3-wrapper' },
  page: { className: 'home-page content3' },
  OverPack: { playScale: 0.3 },
  titleWrapper: {
    className: 'title-wrapper',
    children: [
      {
        name: 'title',
        children: '蚂蚁金融云提供专业的服务',
        className: 'title-h1',
      },
      {
        name: 'content',
        className: 'title-content',
        children: '基于阿里云强大的基础资源',
      },
    ],
  },
  block: {
    className: 'content3-block-wrapper',
    children: [
      {
        name: 'block0',
        className: 'content3-block',
        md: 8,
        xs: 24,
        children: {
          icon: {
            className: 'content3-icon',
            children:
              'https://zos.alipayobjects.com/rmsportal/ScHBSdwpTkAHZkJ.png',
          },
          textWrapper: { className: 'content3-text' },
          title: { className: 'content3-title', children: '企业资源管理' },
          content: {
            className: 'content3-content',
            children:
              '云资源集中编排、弹性伸缩、持续发布和部署，高可用及容灾。',
          },
        },
      },
      {
        name: 'block1',
        className: 'content3-block',
        md: 8,
        xs: 24,
        children: {
          icon: {
            className: 'content3-icon',
            children:
              'https://zos.alipayobjects.com/rmsportal/NKBELAOuuKbofDD.png',
          },
          textWrapper: { className: 'content3-text' },
          title: { className: 'content3-title', children: '云安全' },
          content: {
            className: 'content3-content',
            children:
              '按金融企业安全要求打造的完整云上安全体系，全方位保障金融应用及数据安全。',
          },
        },
      },
      {
        name: 'block2',
        className: 'content3-block',
        md: 8,
        xs: 24,
        children: {
          icon: {
            className: 'content3-icon',
            children:
              'https://zos.alipayobjects.com/rmsportal/xMSBjgxBhKfyMWX.png',
          },
          textWrapper: { className: 'content3-text' },
          title: { className: 'content3-title', children: '云监控' },
          content: {
            className: 'content3-content',
            children:
              '分布式云环境集中监控，统一资源及应用状态视图，智能分析及故障定位。',
          },
        },
      },
      {
        name: 'block3',
        className: 'content3-block',
        md: 8,
        xs: 24,
        children: {
          icon: {
            className: 'content3-icon',
            children:
              'https://zos.alipayobjects.com/rmsportal/MNdlBNhmDBLuzqp.png',
          },
          textWrapper: { className: 'content3-text' },
          title: { className: 'content3-title', children: '移动' },
          content: {
            className: 'content3-content',
            children:
              '一站式移动金融APP开发及全面监控；丰富可用组件，动态发布和故障热修复。',
          },
        },
      },
      {
        name: 'block4',
        className: 'content3-block',
        md: 8,
        xs: 24,
        children: {
          icon: {
            className: 'content3-icon',
            children:
              'https://zos.alipayobjects.com/rmsportal/UsUmoBRyLvkIQeO.png',
          },
          textWrapper: { className: 'content3-text' },
          title: { className: 'content3-title', children: '分布式中间件' },
          content: {
            className: 'content3-content',
            children:
              '金融级联机交易处理中间件，大规模分布式计算机，数万笔/秒级并发能力，严格保证交易数据统一性。',
          },
        },
      },
      {
        name: 'block5',
        className: 'content3-block',
        md: 8,
        xs: 24,
        children: {
          icon: {
            className: 'content3-icon',
            children:
              'https://zos.alipayobjects.com/rmsportal/ipwaQLBLflRfUrg.png',
          },
          textWrapper: { className: 'content3-text' },
          title: { className: 'content3-title', children: '大数据' },
          content: {
            className: 'content3-content',
            children:
              '一站式、全周期大数据协同工作平台，PB级数据处理、毫秒级数据分析工具。',
          },
        },
      },
    ],
  },
};
export const Feature11DataSource = {
  wrapper: { className: 'home-page-wrapper content1-wrapper' },
  OverPack: { className: 'home-page content1', playScale: 0.3 },
  imgWrapper: { className: 'content1-img', md: 10, xs: 24 },
  img: {
    children: 'https://zos.alipayobjects.com/rmsportal/nLzbeGQLPyBJoli.png',
  },
  textWrapper: { className: 'content1-text', md: 14, xs: 24 },
  title: { className: 'content1-title', children: '企业资源管理' },
  content: {
    className: 'content1-content',
    children:
      '云资源集中编排、弹性伸缩、持续发布和部署，高可用及容灾。云资源集中编排、弹性伸缩、持续发布和部署，高可用及容灾。云资源集中编排、弹性伸缩、持续发布和部署，高可用及容灾。',
  },
};
export const Feature20DataSource = {
  wrapper: { className: 'home-page-wrapper content2-wrapper' },
  OverPack: { className: 'home-page content2', playScale: 0.3 },
  imgWrapper: { className: 'content2-img', md: 10, xs: 24 },
  img: {
    children: 'https://zos.alipayobjects.com/rmsportal/tvQTfCupGUFKSfQ.png',
  },
  textWrapper: { className: 'content2-text', md: 14, xs: 24 },
  title: { className: 'content2-title', children: '分布式中间件' },
  content: {
    className: 'content2-content',
    children:
      '金融级联机交易处理中间件，大规模分布式计算机，数万笔/秒级并发能力，严格保证交易数据统一性。金融级联机交易处理中间件，大规模分布式计算机，数万笔/秒级并发能力，严格保证交易数据统一性。',
  },
};
export const Content50DataSource = {
  wrapper: { className: 'home-page-wrapper content5-wrapper' },
  page: { className: 'home-page content5' },
  OverPack: { playScale: 0.3, className: '' },
  titleWrapper: {
    className: 'title-wrapper',
    children: [
      { name: 'title', children: '分析模型', className: 'title-h1' },
      {
        name: 'content',
        className: 'title-content',
        children: '涵盖 标准物质不确定度评定 与 检测数据处理 等多种类型',
      },
    ],
  },
  block: {
    className: 'feature7-block-wrapper',
    gutter: 24, children: [
      {
        md: 6,
        xs: 24,
        name: 'block0',
        className: 'feature7-block',
        children: {
          className: 'feature7-block-group',
          children: [
            {
              name: 'image',
              className: 'feature7-block-image',
              children:
                'https://gw.alipayobjects.com/zos/basement_prod/e339fc34-b022-4cde-9607-675ca9e05231.svg',
            },
            {
              name: 'title',
              className: 'feature7-block-title',
              children: '身份证',
            },
            {
              name: 'content',
              className: 'feature7-block-content',
              children: '识别身份证正反面姓名、身份证号、发证机关等相关信息',
            },
          ],
        },
      },
      {
        md: 6,
        xs: 24,
        name: 'block1',
        className: 'feature7-block',
        children: {
          className: 'feature7-block-group',
          children: [
            {
              name: 'image',
              className: 'feature7-block-image',
              children:
                'https://gw.alipayobjects.com/zos/basement_prod/e339fc34-b022-4cde-9607-675ca9e05231.svg',
            },
            {
              name: 'title',
              className: 'feature7-block-title',
              children: '身份证',
            },
            {
              name: 'content',
              className: 'feature7-block-content',
              children: '识别身份证正反面姓名、身份证号、发证机关等相关信息',
            },
          ],
        },
      },
      {
        md: 6,
        xs: 24,
        name: 'block2',
        className: 'feature7-block',
        children: {
          className: 'feature7-block-group',
          children: [
            {
              name: 'image',
              className: 'feature7-block-image',
              children:
                'https://gw.alipayobjects.com/zos/basement_prod/e339fc34-b022-4cde-9607-675ca9e05231.svg',
            },
            {
              name: 'title',
              className: 'feature7-block-title',
              children: '身份证',
            },
            {
              name: 'content',
              className: 'feature7-block-content',
              children: '识别身份证正反面姓名、身份证号、发证机关等相关信息',
            },
          ],
        },
      },
      {
        md: 6,
        xs: 24,
        name: 'block3',
        className: 'feature7-block',
        children: {
          className: 'feature7-block-group',
          children: [
            {
              name: 'image',
              className: 'feature7-block-image',
              children:
                'https://gw.alipayobjects.com/zos/basement_prod/e339fc34-b022-4cde-9607-675ca9e05231.svg',
            },
            {
              name: 'title',
              className: 'feature7-block-title',
              children: '身份证',
            },
            {
              name: 'content',
              className: 'feature7-block-content',
              children: '识别身份证正反面姓名、身份证号、发证机关等相关信息',
            },
          ],
          target: '',
          href: '',
        },
      },
      {
        md: 6,
        xs: 24,
        name: 'block4',
        className: 'feature7-block',
        children: {
          className: 'feature7-block-group',
          children: [
            {
              name: 'image',
              className: 'feature7-block-image',
              children:
                'https://gw.alipayobjects.com/zos/basement_prod/e339fc34-b022-4cde-9607-675ca9e05231.svg',
            },
            {
              name: 'title',
              className: 'feature7-block-title',
              children: '身份证',
            },
            {
              name: 'content',
              className: 'feature7-block-content',
              children: '识别身份证正反面姓名、身份证号、发证机关等相关信息',
            },
          ],
        },
      },
      {
        md: 6,
        xs: 24,
        name: 'block5',
        className: 'feature7-block',
        children: {
          className: 'feature7-block-group',
          children: [
            {
              name: 'image',
              className: 'feature7-block-image',
              children:
                'https://gw.alipayobjects.com/zos/basement_prod/e339fc34-b022-4cde-9607-675ca9e05231.svg',
            },
            {
              name: 'title',
              className: 'feature7-block-title',
              children: '身份证',
            },
            {
              name: 'content',
              className: 'feature7-block-content',
              children: '识别身份证正反面姓名、身份证号、发证机关等相关信息',
            },
          ],
        },
      },
      {
        md: 6,
        xs: 24,
        name: 'block6',
        className: 'feature7-block',
        children: {
          className: 'feature7-block-group',
          children: [
            {
              name: 'image',
              className: 'feature7-block-image',
              children:
                'https://gw.alipayobjects.com/zos/basement_prod/e339fc34-b022-4cde-9607-675ca9e05231.svg',
            },
            {
              name: 'title',
              className: 'feature7-block-title',
              children: '身份证',
            },
            {
              name: 'content',
              className: 'feature7-block-content',
              children: '识别身份证正反面姓名、身份证号、发证机关等相关信息',
            },
          ],
        },
      },
      {
        md: 6,
        xs: 24,
        name: 'block7',
        className: 'feature7-block',
        children: {
          className: 'feature7-block-group',
          children: [
            {
              name: 'image',
              className: 'feature7-block-image',
              children:
                'https://gw.alipayobjects.com/zos/basement_prod/e339fc34-b022-4cde-9607-675ca9e05231.svg',
            },
            {
              name: 'title',
              className: 'feature7-block-title',
              children: '身份证',
            },
            {
              name: 'content',
              className: 'feature7-block-content',
              children: '识别身份证正反面姓名、身份证号、发证机关等相关信息',
            },
          ],
        },
      },
    ],
    // children: [
    //   {
    //     name: 'block0',
    //     className: 'block',
    //     md: 6,
    //     xs: 24,
    //     children: {
    //       wrapper: { className: 'content5-block-content' },
    //       img: {
    //         children:
    //           '/favicon.ico',
    //       },
    //       content: { children: '多家实验室联合定值模式定值不确定度uchar评定实例。对多家实验室联合定值不确定度进行计算。输入一组样本值，最终输出不确定度结果。' },
    //     },
    //   },
    //   {
    //     name: 'block1',
    //     className: 'block',
    //     md: 6,
    //     xs: 24,
    //     children: {
    //       wrapper: { className: 'content5-block-content' },
    //       img: {
    //         children:
    //           'https://zos.alipayobjects.com/rmsportal/faKjZtrmIbwJvVR.svg',
    //       },
    //       content: { children: 'Ant Motion' },
    //     },
    //   },
    //   {
    //     name: 'block2',
    //     className: 'block',
    //     md: 6,
    //     xs: 24,
    //     children: {
    //       wrapper: { className: 'content5-block-content' },
    //       img: {
    //         children:
    //           'https://t.alipayobjects.com/images/rmsweb/T11aVgXc4eXXXXXXXX.svg',
    //       },
    //       content: { children: 'Ant Design' },
    //     },
    //   },
    //   {
    //     name: 'block3',
    //     className: 'block',
    //     md: 6,
    //     xs: 24,
    //     children: {
    //       wrapper: { className: 'content5-block-content' },
    //       img: {
    //         children:
    //           'https://zos.alipayobjects.com/rmsportal/faKjZtrmIbwJvVR.svg',
    //       },
    //       content: { children: 'Ant Motion' },
    //     },
    //   },
    //   {
    //     name: 'block4',
    //     className: 'block',
    //     md: 6,
    //     xs: 24,
    //     children: {
    //       wrapper: { className: 'content5-block-content' },
    //       img: {
    //         children:
    //           'https://t.alipayobjects.com/images/rmsweb/T11aVgXc4eXXXXXXXX.svg',
    //       },
    //       content: { children: 'Ant Design' },
    //     },
    //   },
    //   {
    //     name: 'block5',
    //     className: 'block',
    //     md: 6,
    //     xs: 24,
    //     children: {
    //       wrapper: { className: 'content5-block-content' },
    //       img: {
    //         children:
    //           'https://zos.alipayobjects.com/rmsportal/faKjZtrmIbwJvVR.svg',
    //       },
    //       content: { children: 'Ant Motion' },
    //     },
    //   },
    //   {
    //     name: 'block6',
    //     className: 'block',
    //     md: 6,
    //     xs: 24,
    //     children: {
    //       wrapper: { className: 'content5-block-content' },
    //       img: {
    //         children:
    //           'https://t.alipayobjects.com/images/rmsweb/T11aVgXc4eXXXXXXXX.svg',
    //       },
    //       content: { children: 'Ant Design' },
    //     },
    //   },
    //   {
    //     name: 'block7',
    //     className: 'block',
    //     md: 6,
    //     xs: 24,
    //     children: {
    //       wrapper: { className: 'content5-block-content' },
    //       img: {
    //         children:
    //           'https://zos.alipayobjects.com/rmsportal/faKjZtrmIbwJvVR.svg',
    //       },
    //       content: { children: '多家实验室联合定值模式定值不确定度uchar评定实例。对多家实验室联合定值不确定度进行计算。输入一组样本值，最终输出不确定度结果。' },
    //     },
    //   },
    // ],
  },
};
export const Feature70DataSource = {
  wrapper: { className: 'home-page-wrapper feature7-wrapper' },
  page: { className: 'home-page feature7' },
  OverPack: { playScale: 0.3 },
  titleWrapper: {
    className: 'feature7-title-wrapper',
    children: [
      {
        name: 'title',
        className: 'feature7-title-h1',
        children: '图像在线服务',
      },
      {
        name: 'content',
        className: 'feature7-title-content',
        children: '你可以直接快速接入图像能力',
      },
    ],
  },
  blockWrapper: {
    className: 'feature7-block-wrapper',
    gutter: 24,
    children: [
      {
        md: 6,
        xs: 24,
        name: 'block0',
        className: 'feature7-block',
        children: {
          className: 'feature7-block-group',
          children: [
            {
              name: 'image',
              className: 'feature7-block-image',
              children:
                'https://gw.alipayobjects.com/zos/basement_prod/e339fc34-b022-4cde-9607-675ca9e05231.svg',
            },
            {
              name: 'title',
              className: 'feature7-block-title',
              children: '身份证',
            },
            {
              name: 'content',
              className: 'feature7-block-content',
              children: '识别身份证正反面姓名、身份证号、发证机关等相关信息',
            },
          ],
        },
      },
      {
        md: 6,
        xs: 24,
        name: 'block1',
        className: 'feature7-block',
        children: {
          className: 'feature7-block-group',
          children: [
            {
              name: 'image',
              className: 'feature7-block-image',
              children:
                'https://gw.alipayobjects.com/zos/basement_prod/e339fc34-b022-4cde-9607-675ca9e05231.svg',
            },
            {
              name: 'title',
              className: 'feature7-block-title',
              children: '身份证',
            },
            {
              name: 'content',
              className: 'feature7-block-content',
              children: '识别身份证正反面姓名、身份证号、发证机关等相关信息',
            },
          ],
        },
      },
      {
        md: 6,
        xs: 24,
        name: 'block2',
        className: 'feature7-block',
        children: {
          className: 'feature7-block-group',
          children: [
            {
              name: 'image',
              className: 'feature7-block-image',
              children:
                'https://gw.alipayobjects.com/zos/basement_prod/e339fc34-b022-4cde-9607-675ca9e05231.svg',
            },
            {
              name: 'title',
              className: 'feature7-block-title',
              children: '身份证',
            },
            {
              name: 'content',
              className: 'feature7-block-content',
              children: '识别身份证正反面姓名、身份证号、发证机关等相关信息',
            },
          ],
        },
      },
      {
        md: 6,
        xs: 24,
        name: 'block3',
        className: 'feature7-block',
        children: {
          className: 'feature7-block-group',
          children: [
            {
              name: 'image',
              className: 'feature7-block-image',
              children:
                'https://gw.alipayobjects.com/zos/basement_prod/e339fc34-b022-4cde-9607-675ca9e05231.svg',
            },
            {
              name: 'title',
              className: 'feature7-block-title',
              children: '身份证',
            },
            {
              name: 'content',
              className: 'feature7-block-content',
              children: '识别身份证正反面姓名、身份证号、发证机关等相关信息',
            },
          ],
          target: '',
          href: '',
        },
      },
      {
        md: 6,
        xs: 24,
        name: 'block4',
        className: 'feature7-block',
        children: {
          className: 'feature7-block-group',
          children: [
            {
              name: 'image',
              className: 'feature7-block-image',
              children:
                'https://gw.alipayobjects.com/zos/basement_prod/e339fc34-b022-4cde-9607-675ca9e05231.svg',
            },
            {
              name: 'title',
              className: 'feature7-block-title',
              children: '身份证',
            },
            {
              name: 'content',
              className: 'feature7-block-content',
              children: '识别身份证正反面姓名、身份证号、发证机关等相关信息',
            },
          ],
        },
      },
      {
        md: 6,
        xs: 24,
        name: 'block5',
        className: 'feature7-block',
        children: {
          className: 'feature7-block-group',
          children: [
            {
              name: 'image',
              className: 'feature7-block-image',
              children:
                'https://gw.alipayobjects.com/zos/basement_prod/e339fc34-b022-4cde-9607-675ca9e05231.svg',
            },
            {
              name: 'title',
              className: 'feature7-block-title',
              children: '身份证',
            },
            {
              name: 'content',
              className: 'feature7-block-content',
              children: '识别身份证正反面姓名、身份证号、发证机关等相关信息',
            },
          ],
        },
      },
      {
        md: 6,
        xs: 24,
        name: 'block6',
        className: 'feature7-block',
        children: {
          className: 'feature7-block-group',
          children: [
            {
              name: 'image',
              className: 'feature7-block-image',
              children:
                'https://gw.alipayobjects.com/zos/basement_prod/e339fc34-b022-4cde-9607-675ca9e05231.svg',
            },
            {
              name: 'title',
              className: 'feature7-block-title',
              children: '身份证',
            },
            {
              name: 'content',
              className: 'feature7-block-content',
              children: '识别身份证正反面姓名、身份证号、发证机关等相关信息',
            },
          ],
        },
      },
      {
        md: 6,
        xs: 24,
        name: 'block7',
        className: 'feature7-block',
        children: {
          className: 'feature7-block-group',
          children: [
            {
              name: 'image',
              className: 'feature7-block-image',
              children:
                'https://gw.alipayobjects.com/zos/basement_prod/e339fc34-b022-4cde-9607-675ca9e05231.svg',
            },
            {
              name: 'title',
              className: 'feature7-block-title',
              children: '身份证',
            },
            {
              name: 'content',
              className: 'feature7-block-content',
              children: '识别身份证正反面姓名、身份证号、发证机关等相关信息',
            },
          ],
        },
      },
    ],
  },
};
export const Footer00DataSource = {
  wrapper: { className: 'home-page-wrapper footer0-wrapper' },
  OverPack: { className: 'home-page footer0', playScale: 0.05 },
  copyright: {
    className: 'copyright',
    children: (
      <span>
        ©2020 <a href="/">UES</a> All Rights
        Reserved
      </span>
    ),
  },
};

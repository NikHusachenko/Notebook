import { Menu, MenuProps } from "antd";
import React from "react";
import { useNavigate } from "react-router-dom";

type MenuItem = Required<MenuProps>['items'][number];

const Header: React.FC = () => {
    const navigate = useNavigate();

    const items: MenuItem[] = [
        { key: 'home', label: 'Home', onClick: () => navigate('/') },
        { key: 'blogs', label: 'Blogs', onClick: () => navigate('/blogs') },
        { key: 'my-blog', label: 'My Blog', onClick: () => navigate('/blog') },
        { key: 'account', label: 'Account', type: 'submenu', children: [
            {
                key: 'account-information',
                type: 'item',
                label: 'Information',
                onClick: () => navigate('account/information')
            },
            { 
                key: 'account-settings', 
                type: 'item', 
                label: 'Settings',
                onClick: () => navigate('account/settings')
            },
            { 
                key: 'log-out',
                type: 'item',
                label: 'Log Out',
                onClick: () => navigate('authentication/log-out')
            }
        ] }
    ];

    return (
        <Menu theme="dark" mode="horizontal" items={items} style={HeaderStyle} />
    );
}

const HeaderStyle : React.CSSProperties = {
    justifyContent: 'center',
}

export default Header;
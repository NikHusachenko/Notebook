import { Menu, MenuProps } from "antd";
import React from "react";

type MenuItem = Required<MenuProps>['items'][number];

const items: MenuItem[] = [
    { key: 'home', label: 'Home' },
    { key: 'blogs', label: 'Blogs' },
    { key: 'my-blog', label: 'My Blog' },
    { key: 'account', label: 'Account', type: 'submenu' ,children: [
        {
            key: 'account-information',
            type: 'item',
            label: 'Information'
        },
        { 
            key: 'account-settings', 
            type: 'item', 
            label: 'Settings'
        },
        { 
            key: 'log-out',
            type: 'item',
            label: 'Log Out',
        }
    ] }
];

const Header: React.FC = () => {
    return (
        <Menu theme="dark" mode="horizontal" items={items} />
    );
}

export default Header;
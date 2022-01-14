import { Icon } from '@iconify/react';
import pieChart2Fill from '@iconify/icons-eva/pie-chart-2-fill';
import peopleFill from '@iconify/icons-eva/people-fill';
import messageSquareFill from '@iconify/icons-eva/message-square-fill';
import homeFill from '@iconify/icons-eva/home-fill';
import fileTextFill from '@iconify/icons-eva/file-text-fill';

// ----------------------------------------------------------------------

const getIcon = (name) => <Icon icon={name} width={22} height={22} />;

const sidebarConfig = [
    {
        title: 'Ana Sayfa',
        path: '/admin/index',
        icon: getIcon(pieChart2Fill)
    },
    {
        title: 'Mesajlar',
        path: '/admin/messages',
        icon: getIcon(messageSquareFill)
    },
    {
        title: 'Kullanıcılar',
        path: '/admin/users',
        icon: getIcon(peopleFill)
    },
    {
        title: 'Evler',
        path: '/admin/houses',
        icon: getIcon(homeFill)
    },
    {
        title: 'Faturalar ve Aidatlar',
        path: '/admin/bills',
        icon: getIcon(fileTextFill)
    }
];

export default sidebarConfig;

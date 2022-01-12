import { Icon } from '@iconify/react';
import pieChart2Fill from '@iconify/icons-eva/pie-chart-2-fill';
import messageSquareFill from '@iconify/icons-eva/message-square-fill';
import fileTextFill from '@iconify/icons-eva/file-text-fill';

// ----------------------------------------------------------------------

const getIcon = (name) => <Icon icon={name} width={22} height={22} />;

const sidebarConfig = [
    {
        title: 'Ana Sayfa',
        path: '/dashboard/index',
        icon: getIcon(pieChart2Fill)
    },
    {
        title: 'Faturalar',
        path: '/dashboard/bills',
        icon: getIcon(fileTextFill)
    },
    {
        title: 'Mesajlar',
        path: '/dashboard/messages',
        icon: getIcon(messageSquareFill)
    }
];

export default sidebarConfig;

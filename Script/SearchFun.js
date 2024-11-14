const products = [
    {
        name: 'Hoa cúc họa mi',
        priceOld: '299.000đ',
        priceNew: '199.000đ',
        img: './Home/HOACUCHOAMI.jpg',
        category: 'wedding',
        color: 'bright'
    },
    {
        name: 'Hoa Hồng',
        priceOld: '299.000đ',
        priceNew: '199.000đ',
        img: './Home/HOAHONGHONG.jpg',
        category: 'birthday',
        color: 'dark'
    },
    {
        name: 'Hoa Cẩm Tú Cầu',
        priceOld: '299.000đ',
        priceNew: '199.000đ',
        img: './Order/CAMTUCAU5LOAI.jpg',
        category: 'opening',
        color: 'multicolor'
    },
    {
        name: 'Hoa Cúc TANA',
        priceOld: '299.000đ',
        priceNew: '199.000đ',
        img: './Order/HOACUCTANA.jpg',
        category: 'opening',
        color: 'multicolor'
    },
    {
        name: 'Hoa Tulip Trắng',
        priceOld: '299.000đ',
        priceNew: '199.000đ',
        img: './Order/HOATULIPTRANG.jpg',
        category: 'birthday',
        color: 'other'
    },
    {
        name: 'Hoa Tulip',
        priceOld: '299.000đ',
        priceNew: '199.000đ',
        img: './Order/HOATULIP.jpg',
        category: 'opening',
        color: 'other'
    },
    {
        name: 'Hoa Hướng Dương',
        priceOld: '299.000đ',
        priceNew: '199.000đ',
        img: './Order/HOAHUONGDUONG.jpg',
        category: 'opening',
        color: 'bright'
    },
    {
        name: 'Hoa Hồng Vàng',
        priceOld: '299.000đ',
        priceNew: '199.000đ',
        img: './Order/HOAHONGVANG.jpg',
        category: 'birthday',
        color: 'bright'
    },
    {
        name: 'Hoa Hồng Tím',
        priceOld: '299.000đ',
        priceNew: '199.000đ',
        img: './Order/HOAHONGTIM.jpg',
        category: 'birthday',
        color: 'bright'
    },
    {
        name: 'Hoa Hồng',
        priceOld: '299.000đ',
        priceNew: '199.000đ',
        img: './Order/HOAHONGDOTRANG.jpg',
        category: 'birthday',
        color: 'multicolor'
    },


    // Add more products as needed
];

// Search function
function searchFlowers() {
    const searchInput = document.getElementById('searchInput').value.toLowerCase();
    const colorFilter = document.getElementById('colorFilter').value;
    const themeFilter = document.getElementById('themeFilter').value;
    
    const filteredProducts = products.filter(product => {
        return (
            product.name.toLowerCase().includes(searchInput) &&
            (colorFilter === 'none' || product.color === colorFilter) &&
            (themeFilter === 'none' || product.category === themeFilter)
        );
    });

    displayProducts(filteredProducts);
}

// Display products
function displayProducts(filteredProducts) {
    const productList = document.getElementById('productList');
    productList.innerHTML = ''; // Clear current product list

    filteredProducts.forEach(product => {
        const productItem = document.createElement('div');
        productItem.classList.add('container__col-item');
        productItem.innerHTML = `
            <div class="product__item-img" style="background-image: url('${product.img}')"></div>
            <div class="product__item-name"><h6>${product.name}</h6></div>
            <div class="product__item-price">
                <span class="product__item-price-old">${product.priceOld}</span>
                <span class="product__item-price-new">${product.priceNew}</span>
            </div>
            <div class="add-to-cart">
                <button class="btn-add-to-cart">Cart</button>
            </div>
        `;
        productList.appendChild(productItem);
    });
}

// Initialize the product display on page load
window.onload = () => {
    displayProducts(products);
};
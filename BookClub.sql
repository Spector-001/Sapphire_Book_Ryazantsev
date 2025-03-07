USE [master]
GO
/****** Object:  Database [BookClub]    Script Date: 28.02.2025 23:07:59 ******/
CREATE DATABASE [BookClub]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BookClub', FILENAME = N'D:\Work 2\Новая папка (3)\BookClub.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'BookClub_log', FILENAME = N'D:\Work 2\Новая папка (3)\BookClub_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [BookClub] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BookClub].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BookClub] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BookClub] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BookClub] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BookClub] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BookClub] SET ARITHABORT OFF 
GO
ALTER DATABASE [BookClub] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [BookClub] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BookClub] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BookClub] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BookClub] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BookClub] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BookClub] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BookClub] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BookClub] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BookClub] SET  ENABLE_BROKER 
GO
ALTER DATABASE [BookClub] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BookClub] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BookClub] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BookClub] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BookClub] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BookClub] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BookClub] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BookClub] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [BookClub] SET  MULTI_USER 
GO
ALTER DATABASE [BookClub] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BookClub] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BookClub] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BookClub] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BookClub] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [BookClub] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [BookClub] SET QUERY_STORE = OFF
GO
USE [BookClub]
GO
/****** Object:  Table [dbo].[Books]    Script Date: 28.02.2025 23:08:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Books](
	[ProductID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Description] [text] NULL,
	[Category] [varchar](50) NOT NULL,
	[Image] [nvarchar](250) NULL,
	[Author] [varchar](255) NULL,
	[Genre] [varchar](100) NULL,
	[Status] [varchar](50) NULL,
	[Shelf] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderItems]    Script Date: 28.02.2025 23:08:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderItems](
	[OrderItemID] [int] IDENTITY(1,1) NOT NULL,
	[OrderID] [int] NOT NULL,
	[UsersID] [int] NULL,
	[ProductID] [int] NOT NULL,
	[CustomersID] [int] NULL,
 CONSTRAINT [PK__OrderIte__57ED06A11D6439F0] PRIMARY KEY CLUSTERED 
(
	[OrderItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 28.02.2025 23:08:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[OrderID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerID] [int] NOT NULL,
	[OrderDate] [datetime] NULL,
	[Total] [decimal](10, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Readers]    Script Date: 28.02.2025 23:08:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Readers](
	[CustomerID] [int] NOT NULL,
	[Username] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
 CONSTRAINT [PK__Customer__A4AE64B8977B60D9] PRIMARY KEY CLUSTERED 
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 28.02.2025 23:08:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[Role] [varchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Books] ON 

INSERT [dbo].[Books] ([ProductID], [Name], [Description], [Category], [Image], [Author], [Genre], [Status], [Shelf]) VALUES (1, N'Гарри Поттер и Философский Камень', N'

"Гарри Поттер и Философский камень" — это первая книга в серии Дж. К. Рoулинг о мальчике-волшебнике. История рассказывает о Гарри Поттере, который до 11 лет жил с неласковыми родственниками, не подозревая о своём истинном происхождении. После того как он узнаёт, что является волшебником, его жизнь меняется кардинально. Гарри поступает в Школу Чародейства Хогвартс, где находит настоящих друзей — Рона Уизли и Гермионы Грейнджер, а также открывает для себя мир магии, полный чудес и опасностей.

В центре сюжета — поиск Философского камня, древнего артефакта, дающего вечную жизнь и неиссякаемые богатства. Гарри и его друзья должны защитить камень от зла, которое стремится воскресить темного мага Лорда Вольдеморта. Это захватывающее приключение учит дружбе, храбрости и вере в себя, а также раскрывает тайны прошлого главного героя.', N'Книга', N'GarryPotter.jpg', N'Дж.К.Роулинг', N'Фэнтези', N'Есть в наличие', N'A1')
INSERT [dbo].[Books] ([ProductID], [Name], [Description], [Category], [Image], [Author], [Genre], [Status], [Shelf]) VALUES (2, N'1984', N'Антиутопический роман Джорджа Оруэлла, рассказывающий о тоталитарном обществе будущего, где личная свобода полностью подавлена. Действие происходит в Океании, одной из трех сверхдержав мира, где правит Партия во главе с Большим Братом. Главный герой, Уинстон Смит, работает в Министерстве Правды, занимаясь фальсификацией истории. Он живет в постоянном страхе перед системой-surveillance, где каждый шаг наблюдается через телесcreens, а мысли контролируются Thought Police.

Уинстон начинает тайно бороться против режима, записывая свои мысли в дневник и вступая в запрещенную любовную связь с Жуланой. Однако их попытка найти свободу приводит к трагическому концу. Роман предупреждает о опасностях тоталитаризма, манипуляции информацией и утраты человеческой индивидуальности. "1984" остается актуальным предостережением о ценности правды и свободы.', N'Книга', N'1984.png', N'Джордж Оруэлл', N'Роман', N'Нету в наличие', N'A2')
INSERT [dbo].[Books] ([ProductID], [Name], [Description], [Category], [Image], [Author], [Genre], [Status], [Shelf]) VALUES (3, N'Война и Мир', N'Эпическое произведение Льва Толстого, сочетающее в себе историческую драму и глубокий психологизм. Действие разворачивается в период Napoleonic Wars, охватывая жизнь русского общества начала XIX века. Роман следует судьбами нескольких семей, главными из которых являются Болконские, Ростовы и Курагины.

Через истории таких героев, как Андрей Болконский, Пьер Безухов и Наташа Ростова, Толстой исследует темы любви, долга, войны и мира, показывая влияние исторических событий на личные жизни людей. Книга пронизана философскими размышлениями о смысле жизни, человеческой природе и роли отдельного человека в потоке истории. "Война и мир" является одним из величайших романов мировой литературы, объединяющим масштабную картину эпохи с тонким анализом человеческих чувств.', N'Книга', N'Войнаимир.png', N'Лев Толстой', N'Роман', N'Есть в наличие', N'A3')
INSERT [dbo].[Books] ([ProductID], [Name], [Description], [Category], [Image], [Author], [Genre], [Status], [Shelf]) VALUES (4, N'Мастер и Маргарита', N'Культовый роман Михаила Булгакова, сочетающий фантастику, сатиру и глубокую философию. Действие происходит в Москве 1930-х годов, когда в город приезжает таинственный иностранец Воланд со своей свитой, состоящей из демонических существ. Прибытие этого темного мага вызывает хаос и разоблачает истинные лица многих обитателей города.

Параллельно развивается история неизвестного писателя, называемого Мастером, который написал роман о Понтии Пилате и Иисусе Христе, но был вынужден отказаться от своего творения под давлением сталинской цензуры. Его любовь к Маргарите становится центральной линией повествования; она готова на всё, чтобы спасти своего возлюбленного, даже заключить договор с дьяволом.

Роман затрагивает вечные темы добра и зла, свободы творчества, верности и искупления. "Мастер и Маргарита" считается одним из самых загадочных и многогранных произведений русской литературы XX века.', N'Книга', N'МастериМаргарита.png', N'Михаил Булгаков', N'Фэнтези', N'Есть в наличие', N'A4')
INSERT [dbo].[Books] ([ProductID], [Name], [Description], [Category], [Image], [Author], [Genre], [Status], [Shelf]) VALUES (5, N'Преступление и наказание', N'Культовый роман Михаила Булгакова, сочетающий фантастику, сатиру и глубокую философию. Действие происходит в Москве 1930-х годов, когда в город приезжает таинственный иностранец Воланд со своей свитой, состоящей из демонических существ. Прибытие этого темного мага вызывает хаос и разоблачает истинные лица многих обитателей города.

Параллельно развивается история неизвестного писателя, называемого Мастером, который написал роман о Понтии Пилате и Иисусе Христе, но был вынужден отказаться от своего творения под давлением сталинской цензуры. Его любовь к Маргарите становится центральной линией повествования; она готова на всё, чтобы спасти своего возлюбленного, даже заключить договор с дьяволом.

Роман затрагивает вечные темы добра и зла, свободы творчества, верности и искупления. "Мастер и Маргарита" считается одним из самых загадочных и многогранных произведений русской литературы XX века.






', N'Книга', N'ПреступлениеИНаказание.png', N'Фёдор Достоевский', N'Роман', N'Нету в наличие', N'A1.C5')
INSERT [dbo].[Books] ([ProductID], [Name], [Description], [Category], [Image], [Author], [Genre], [Status], [Shelf]) VALUES (6, N'Ночь живых мертвецов', N'Захватывающая книга в жанре ужасов, погружающий читателя в атмосферу страха и безысходности. История рассказывает о внезапном распространении эпидемии, превращающей людей в кровожадных зомби. Группа выживших объединяется в попытке спастись от орд нежити, запираясь в заброшенном доме. Здесь они сталкиваются не только с внешней опасностью, но и с внутренними страхами, паранойей и конфликтами.

Произведение нагнетает напряжение через реалистичное описание зомби-апокалипсиса и психологическое давление на персонажей. Оно исследует темные стороны человеческой природы, показывая, как люди действуют в условиях тотальной опасности. "Ночь живых мертвецов" стала классическим примером хоррора, где главный ужас заключается не только в монстрах, но и в самом человеке.', N'Книга', N'НочиЖивыхМертвецов.png', N'Джордж А. Ромеро', N'Ужасы', N'Есть в наличие', N'A1.C6')
INSERT [dbo].[Books] ([ProductID], [Name], [Description], [Category], [Image], [Author], [Genre], [Status], [Shelf]) VALUES (7, N'Берсерк', N'Эпическая темная фэнтези-манга Кэнтаро Миуры, погружающая читателей в мрачный и жестокий мир средневековья, полный насилия, демонов и трагедий. Главный герой, Гатс, бывший преданный воин, теперь проклятый и одинокий боец с огромным двуручным мечом, странствует в поисках справедливости и понимания своей судьбы.
История начинается с дружбы Гатса и Гриффита, лидера банды наемников, мечтающего о королевстве. Однако после того как Гриффит заключает сделку с демонами, его душа изменяется, и их пути расходятся. Гатс, проклятый и превратившийся в Berserker, продолжает бороться с демонами и ужасными существами, сталкиваясь с вопросами о предательстве, дружбе и человеческой природе.
"Берсерк" сочетает мощные батальные сцены с глубокой философией, создавая атмосферу безысходности и надежды. Манга получила признание как одна из величайших работ в жанре, захватывающая своим мрачным миром и непростыми героями.

', N'Манга', N'Берсерк.png', N'Кентаро Миур', N'Фэнтези', N'Нету в наличие', N'A7')
INSERT [dbo].[Books] ([ProductID], [Name], [Description], [Category], [Image], [Author], [Genre], [Status], [Shelf]) VALUES (8, N'Тетрадь Смерти', N'Психологический триллер в жанре манги, созданный Цугуми Оба. История начинается с того, что школьник Ягами Лайт находит таинственную тетрадь, которая позволяет убивать людей, записывая их имена. Убежденный в необходимости очистить мир от преступности, Лайт начинает использовать тетрадь, устраняя преступников и приобретая популярность под именем "Кира".

Однако его действия привлекают внимание гениального детектива Л, который подозревает Лайта в убийствах и начинает расследование. Разгорается интеллектуальная битва между Лайтом и Л, где каждый пытается доказать свою правоту. Манга затрагивает глубокие вопросы о справедливости, власти и морали, держа читателей в напряжении до самого конца. "Тетрадь Смерти" стала культовой, благодаря своему запутанному сюжету и философским размышлениям о природе добра и зла.', N'Манга', N'ТетрадьСмерти.png', N'Тайто Такахаси', N'Триллер', N'Есть в наличие', N'A8')
INSERT [dbo].[Books] ([ProductID], [Name], [Description], [Category], [Image], [Author], [Genre], [Status], [Shelf]) VALUES (9, N'Психопаспорт', N'Интеллектуальный триллер, созданный компанией Production I.G, который погружает зрителей в футуристический мир, где технологии позволяют измерять уровень психического здоровья граждан через показатель "Сигма". Этот показатель отражает уровень стресса, агрессии и эмоционального состояния каждого человека. Люди с высоким уровнем Сигмы считаются потенциально опасными и становятся целью полиции, использующей специальные устройства для поддержания порядка.

Главный герой, инспектор Синъя Кагэёси, работает в полиции с помощью инструментов, которые помогают ему определять преступников по их психическому состоянию. Вместе со своим напарником и роботом-ассистентом он расследует дела, сталкиваясь с моральными дилеммами и вопросами о справедливости системы. Сюжет полон интриг, тайных заговоров и непредсказуемых поворотов, заставляющих задуматься о границах свободы, контроля и этики в высоко технологизированном обществе.

"Псycho-Pass" сочетает напряженные сцены и глубокие философские вопросы, создавая атмосферу постоянного напряжения и интеллектуального возбуждения. Это произведение стало культовым в жанре триллера, вызывая у зрителей и читателей множество дискуссий о будущем и человеческой природе.', N'Манга', N'ПсихоПаспорт.png', N'Проект Psycho-Pass', N'Триллер', N'Есть в наличие', N'A9')
INSERT [dbo].[Books] ([ProductID], [Name], [Description], [Category], [Image], [Author], [Genre], [Status], [Shelf]) VALUES (11, N'Коллекция ужасов Дзюндзи Ито', N'Сборник коротких историй, полных мистики, ужасов и черного юмора, который погружает читателей в мир непостижимых и мрачных тайн. Каждая история сама по себе уникальна, но все они объединены общим тоном нереальности и гротеска, свойственным автору.

Дзюндзи Ито мастерски сочетает элементы классического ужаса с элементами фантастики и мистики, создавая ситуации, которые заставляют задуматься о границах реальности и человеческого разума. Его персонажи сталкиваются с невообразимыми существами и явлениями, часто выходящими за рамки логики и понимания.

В каждой истории Ито исследует темы одиночества, безумия и тайных сторон человеческой природы. Его работы зачастую содержат неожиданные повороты и концовки, оставляющие глубокое впечатление. "Коллекция ужасов" завораживает своей оригинальностью и способностью вызывать как страх, так и восхищение перед необычностью и тонкостью сюжетов.

Эта манга является обязательным чтением для поклонников жанра ужасов и тех, кто ценит непредсказуемость и философскую глубину в литературе.', N'Манга', N'КоллекцияУжасовДзюндзиИто.png', N'Дзюнъидзи Ито', N'Ужасы', N'Нету в наличие', N'A10')
SET IDENTITY_INSERT [dbo].[Books] OFF
GO
SET IDENTITY_INSERT [dbo].[Orders] ON 

INSERT [dbo].[Orders] ([OrderID], [CustomerID], [OrderDate], [Total]) VALUES (1, 1, CAST(N'2024-01-01T00:00:00.000' AS DateTime), CAST(1.00 AS Decimal(10, 2)))
INSERT [dbo].[Orders] ([OrderID], [CustomerID], [OrderDate], [Total]) VALUES (2, 2, CAST(N'2024-01-02T00:00:00.000' AS DateTime), CAST(1.00 AS Decimal(10, 2)))
INSERT [dbo].[Orders] ([OrderID], [CustomerID], [OrderDate], [Total]) VALUES (3, 3, CAST(N'2024-01-03T00:00:00.000' AS DateTime), CAST(2.00 AS Decimal(10, 2)))
INSERT [dbo].[Orders] ([OrderID], [CustomerID], [OrderDate], [Total]) VALUES (4, 4, CAST(N'2024-01-04T00:00:00.000' AS DateTime), CAST(1.00 AS Decimal(10, 2)))
INSERT [dbo].[Orders] ([OrderID], [CustomerID], [OrderDate], [Total]) VALUES (5, 5, CAST(N'2024-01-05T00:00:00.000' AS DateTime), CAST(1.00 AS Decimal(10, 2)))
INSERT [dbo].[Orders] ([OrderID], [CustomerID], [OrderDate], [Total]) VALUES (6, 6, CAST(N'2024-01-06T00:00:00.000' AS DateTime), CAST(3.00 AS Decimal(10, 2)))
INSERT [dbo].[Orders] ([OrderID], [CustomerID], [OrderDate], [Total]) VALUES (7, 7, CAST(N'2024-01-07T00:00:00.000' AS DateTime), CAST(1.00 AS Decimal(10, 2)))
INSERT [dbo].[Orders] ([OrderID], [CustomerID], [OrderDate], [Total]) VALUES (8, 8, CAST(N'2024-01-08T00:00:00.000' AS DateTime), CAST(2.00 AS Decimal(10, 2)))
INSERT [dbo].[Orders] ([OrderID], [CustomerID], [OrderDate], [Total]) VALUES (9, 9, CAST(N'2024-09-01T00:00:00.000' AS DateTime), CAST(1.00 AS Decimal(10, 2)))
INSERT [dbo].[Orders] ([OrderID], [CustomerID], [OrderDate], [Total]) VALUES (10, 10, CAST(N'2024-01-09T00:00:00.000' AS DateTime), CAST(2.00 AS Decimal(10, 2)))
SET IDENTITY_INSERT [dbo].[Orders] OFF
GO
INSERT [dbo].[Readers] ([CustomerID], [Username], [Password]) VALUES (1, N'Spector', N'12345')
INSERT [dbo].[Readers] ([CustomerID], [Username], [Password]) VALUES (2, N'Readless', N'612234')
INSERT [dbo].[Readers] ([CustomerID], [Username], [Password]) VALUES (3, N'First', N'4125671')
INSERT [dbo].[Readers] ([CustomerID], [Username], [Password]) VALUES (4, N'Uwo', N'5125125')
INSERT [dbo].[Readers] ([CustomerID], [Username], [Password]) VALUES (5, N'Genress', N'1245661')
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserID], [Username], [Password], [Role]) VALUES (1, N'client', N'pass123', N'Клиент')
INSERT [dbo].[Users] ([UserID], [Username], [Password], [Role]) VALUES (2, N'manager1', N'pass456', N'Менеджер')
INSERT [dbo].[Users] ([UserID], [Username], [Password], [Role]) VALUES (3, N'admin1', N'adminpass', N'Администратор')
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[Orders] ADD  DEFAULT (getdate()) FOR [OrderDate]
GO
ALTER TABLE [dbo].[OrderItems]  WITH CHECK ADD  CONSTRAINT [FK_OrderItems_Books] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Books] ([ProductID])
GO
ALTER TABLE [dbo].[OrderItems] CHECK CONSTRAINT [FK_OrderItems_Books]
GO
ALTER TABLE [dbo].[OrderItems]  WITH CHECK ADD  CONSTRAINT [FK_OrderItems_Orders] FOREIGN KEY([OrderID])
REFERENCES [dbo].[Orders] ([OrderID])
GO
ALTER TABLE [dbo].[OrderItems] CHECK CONSTRAINT [FK_OrderItems_Orders]
GO
ALTER TABLE [dbo].[OrderItems]  WITH CHECK ADD  CONSTRAINT [FK_OrderItems_Readers] FOREIGN KEY([CustomersID])
REFERENCES [dbo].[Readers] ([CustomerID])
GO
ALTER TABLE [dbo].[OrderItems] CHECK CONSTRAINT [FK_OrderItems_Readers]
GO
ALTER TABLE [dbo].[OrderItems]  WITH CHECK ADD  CONSTRAINT [FK_OrderItems_Users] FOREIGN KEY([UsersID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[OrderItems] CHECK CONSTRAINT [FK_OrderItems_Users]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD CHECK  (([Role]='Администратор' OR [Role]='Менеджер' OR [Role]='Клиент'))
GO
USE [master]
GO
ALTER DATABASE [BookClub] SET  READ_WRITE 
GO

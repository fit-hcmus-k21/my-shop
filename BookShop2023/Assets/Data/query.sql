USE BookShop2023
GO


-- xem tất cả khóa ngoại trong các bảng
SELECT
    FK.name AS 'ConstraintName',
    TP.name AS 'ParentTable',
    REF.name AS 'ReferencedTable',
    CP.name AS 'ParentColumn',
    REFCP.name AS 'ReferencedColumn'
FROM
    sys.foreign_keys AS FK
INNER JOIN
    sys.tables AS TP ON FK.parent_object_id = TP.object_id
INNER JOIN
    sys.tables AS REF ON FK.referenced_object_id = REF.object_id
INNER JOIN
    sys.foreign_key_columns AS FKC ON FK.object_id = FKC.constraint_object_id
INNER JOIN
    sys.columns AS CP ON FKC.parent_column_id = CP.column_id AND FKC.parent_object_id = CP.object_id
INNER JOIN
    sys.columns AS REFCP ON FKC.referenced_column_id = REFCP.column_id AND FKC.referenced_object_id = REFCP.object_id;



-- Thay đổi ràng buộc khóa ngoại để thực hiện hành động "ON DELETE CASCADE"
ALTER TABLE Product
DROP CONSTRAINT FK_Product_Category;

ALTER TABLE Product
ADD CONSTRAINT FK_Product_Category
FOREIGN KEY (CatID) REFERENCES Category(ID)
ON DELETE CASCADE;


